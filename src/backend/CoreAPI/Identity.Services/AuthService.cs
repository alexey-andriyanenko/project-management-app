using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Identity.Contracts.Exceptions;
using Identity.Contracts.Parameters.Auth;
using Identity.Contracts.Results;
using Identity.Contracts.Services;
using Identity.DataAccess;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Identity.Services;

public class AuthService(IdentityDbContext dbContext, IConfiguration configuration) : IAuthService
{
    public async Task<RegisterResult> RegisterAsync(RegisterParameters parameters)
    {
        var user = new UserEntity()
        {
            Id = Guid.NewGuid(),
            FirstName = parameters.FirstName,
            LastName = parameters.LastName,
            Email = parameters.Email,
            UserName = parameters.UserName,
        };
        
        var passwordHasher = new PasswordHasher<UserEntity>();
        var passwordHash = passwordHasher.HashPassword(user, parameters.Password);
        user.PasswordHash = passwordHash;
        
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        
        var tokens = await GenerateTokensAsync(user);
        
        return new RegisterResult()
        {
            UserId = user.Id,
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
        };
    }
    
    public async Task<LoginResult> LoginAsync(LoginParameters parameters)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == parameters.Email);

        if (user == null)
        {
            throw new UserNotFoundException(parameters.Email);
        }

        var passwordHasher = new PasswordHasher<UserEntity>();
        var verificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, parameters.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new InvalidCredentialsException();
        }

        var tokens = await GenerateTokensAsync(user);
        
        return new LoginResult()
        {
            UserId = user.Id,
            AccessToken = tokens.AccessToken,
            RefreshToken = tokens.RefreshToken,
        };
    }
    
    public async Task<RefreshAccessTokenResult> RefreshAccessTokenAsync(RefreshAccessTokenParameters request)
    {
        var token = await dbContext.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

        if (token == null)
        {
            throw new RefreshTokenNotFoundException();
        }

        if (token.IsExpired)
        {
            throw new RefreshTokenExpiredException();
        }

        if (token.IsRevoked)
        {
            throw new RefreshTokenAlreadyRevokedException();
        }

        if (token.User is null)
        {
            throw new UserNotFoundException(token.UserId);
        }
        
        var newAccessToken = GenerateAccessToken(GetClaims(token.User));
        
        var newRefreshToken = new RefreshTokenEntity()
        {
            UserId = token.UserId,
            Token = GenerateRefreshToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
        };
        
        token.RevokedAt = DateTime.UtcNow;
        token.ReplacedByToken = newRefreshToken.Token;
        
        dbContext.RefreshTokens.Add(newRefreshToken);
        await dbContext.SaveChangesAsync();

        return new RefreshAccessTokenResult()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
        };
    }
    
    private async Task<GenerateTokensResult> GenerateTokensAsync(UserEntity user)
    {
        var accessToken = GenerateAccessToken(GetClaims(user));
        var refreshToken = GenerateRefreshToken();

        dbContext.RefreshTokens.Add(new RefreshTokenEntity()
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
        });
        
        await dbContext.SaveChangesAsync();

        return new GenerateTokensResult()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    private IReadOnlyList<Claim> GetClaims(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.UserName ?? "")
        };
        
        return claims;
    }
    
    private string GenerateAccessToken(IReadOnlyList<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        
        return Convert.ToBase64String(randomBytes);
    }
}