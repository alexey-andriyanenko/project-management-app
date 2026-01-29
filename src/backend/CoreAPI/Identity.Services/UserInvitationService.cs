using System.Security.Cryptography;
using System.Web;
using Identity.Contracts.Dtos;
using Identity.Contracts.Exceptions;
using Identity.Contracts.Parameters.UserInvite;
using Identity.Contracts.Results;
using Identity.Contracts.Services;
using Identity.DataAccess;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Enums;
using Identity.Events.Contracts;
using Identity.Services.Mappings;
using Infrastructure.EventBus.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Identity.Services;

public class UserInvitationService(
    IdentityDbContext dbContext,
    IConfiguration configuration,
    IEventBus eventBus
    ) : IUserInviteService
{
    private const int TokenLength = 32;
    private const int InvitationExpirationDays = 7;

    public async Task<GetManyUserInvitationsResult> GetManyAsync(GetManyUserInvitationsParameters parameters)
    {
        var invitations = await dbContext.UserInvitations
            .AsNoTracking()
            .Where(x => x.TenantId == parameters.TenantId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        var baseUrl = configuration["WebClientUrls:AcceptInvitationUrl"];
        var dtos = invitations.Select(x =>
        {
            var urlEncodedToken = EncodeTokenForUrl(x.Token);
            return x.ToDto($"{baseUrl}?token={urlEncodedToken}");
        }).ToList();

        return new GetManyUserInvitationsResult
        {
            Invitations = dtos
        };
    }

    public async Task<UserInvitationDto> GetAsync(GetUserInvitationParameters parameters)
    {
        var invitation = await dbContext.UserInvitations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == parameters.InvitationId);

        if (invitation == null)
        {
            throw new InvitationNotFoundException(parameters.InvitationId);
        }

        var baseUrl = configuration["WebClientUrls:AcceptInvitationUrl"];
        var urlEncodedToken = EncodeTokenForUrl(invitation.Token);
        return invitation.ToDto($"{baseUrl}?token={urlEncodedToken}");
    }

    public async Task<UserInvitationDto> CreateAsync(CreateUserInvitationParameters parameters)
    {
        var existingInvitation = await dbContext.UserInvitations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId && x.Email == parameters.Email && x.AcceptedAt == null);

        if (existingInvitation != null)
        {
            throw new InvitationAlreadyExistsException(parameters.Email, parameters.TenantId);
        }

        var token = GenerateSecureToken();
        var urlEncodedToken = EncodeTokenForUrl(token);

        var invitation = new UserInvitationEntity
        {
            Id = Guid.NewGuid(),
            TenantId = parameters.TenantId,
            Token = token,
            Email = parameters.Email,
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            TenantMemberRole = Enum.Parse<TenantMemberRole>(parameters.TenantMemberRole),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(InvitationExpirationDays)
        };

        dbContext.UserInvitations.Add(invitation);
        await dbContext.SaveChangesAsync();

        var invitationUrl = configuration["WebClientUrls:AcceptInvitationUrl"];
        return invitation.ToDto($"{invitationUrl}?token={urlEncodedToken}");
    }

    public async Task AcceptAsync(AcceptUserInvitationParameters parameters)
    {
        var token = DecodeTokenFromUrl(parameters.InvitationToken);
        
        var invitation = await dbContext.UserInvitations
            .FirstOrDefaultAsync(x => x.Token == token);

        if (invitation == null)
        {
            throw new InvitationTokenNotFoundException();
        }

        if (invitation.AcceptedAt.HasValue)
        {
            throw new InvitationAlreadyAcceptedException();
        }

        if (DateTime.UtcNow > invitation.ExpiresAt)
        {
            throw new InvitationExpiredException();
        }

        var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == invitation.Email);
        if (existingUser != null)
        {
            throw new UserAlreadyExistsException(invitation.Email);
        }

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            FirstName = invitation.FirstName,
            LastName = invitation.LastName,
            Email = invitation.Email,
            UserName = parameters.UserName,
            EmailConfirmed = true
        };

        var passwordHasher = new PasswordHasher<UserEntity>();
        user.PasswordHash = passwordHasher.HashPassword(user, parameters.Password);

        dbContext.Users.Add(user);
        
        invitation.AcceptedAt = DateTime.UtcNow;
        
        await dbContext.SaveChangesAsync();
        
        await eventBus.PublishAsync(new UserInvitationAcceptedEvent()
        {
            UserId = user.Id,
            TenantId = invitation.TenantId,
            TenantMemberRole = invitation.TenantMemberRole.ToString()
        });
    }

    public async Task ResendAsync(ResendUserInvitationParameters parameters)
    {
        var invitation = await dbContext.UserInvitations
            .FirstOrDefaultAsync(x => x.Id == parameters.InvitationId);

        if (invitation == null)
        {
            throw new InvitationNotFoundException(parameters.InvitationId);
        }

        if (invitation.AcceptedAt.HasValue)
        {
            throw new InvitationAlreadyAcceptedException();
        }

        var token = GenerateSecureToken();
        
        invitation.Token = token;
        invitation.CreatedAt = DateTime.UtcNow;
        invitation.ExpiresAt = DateTime.UtcNow.AddDays(InvitationExpirationDays);

        await dbContext.SaveChangesAsync();
    }

    public async Task<ValidateUserInvitationResult> ValidateAsync(ValidateUserInvitationParameters parameters)
    {
        var token = DecodeTokenFromUrl(parameters.InvitationToken);
        
        var invitation = await dbContext.UserInvitations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Token == token);

        if (invitation == null)
        {
            return new ValidateUserInvitationResult { IsValid = false };
        }

        if (invitation.AcceptedAt.HasValue)
        {
            return new ValidateUserInvitationResult { IsValid = false };
        }

        if (DateTime.UtcNow > invitation.ExpiresAt)
        {
            return new ValidateUserInvitationResult { IsValid = false };
        }

        var existingUser = await dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Email == invitation.Email);

        if (existingUser)
        {
            return new ValidateUserInvitationResult { IsValid = false };
        }

        return new ValidateUserInvitationResult 
        { 
            IsValid = true,
            TenantId = invitation.TenantId,
            FirstName = invitation.FirstName,
            LastName = invitation.LastName,
            Email = invitation.Email,
            TenantMemberRole = invitation.TenantMemberRole.ToString()
        };
    }

    public async Task DeleteAsync(DeleteUserInvitationParameters parameters)
    {
        var invitation = await dbContext.UserInvitations
            .FirstOrDefaultAsync(x => x.Id == parameters.InvitationId);

        if (invitation == null)
        {
            throw new InvitationNotFoundException(parameters.InvitationId);
        }

        dbContext.UserInvitations.Remove(invitation);
        await dbContext.SaveChangesAsync();
    }

    private static string GenerateSecureToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(TokenLength);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

    private static string EncodeTokenForUrl(string token)
    {
        return HttpUtility.UrlEncode(token);
    }

    private static string DecodeTokenFromUrl(string urlEncodedToken)
    {
        return HttpUtility.UrlDecode(urlEncodedToken);
    }
}
