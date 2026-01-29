using Facade.IdentityManagement.Contracts.Dtos;
using Facade.IdentityManagement.Contracts.Services;
using Facade.IdentityManagement.Services.Mappings.Dtos;
using Facade.IdentityManagement.Services.Mappings.Parameters.UserInvitation;

namespace Facade.IdentityManagement.Services;

public class UserInvitationManagementService(
    Identity.Client.Contracts.IIdentityClient identityClient,
    Tenant.Client.Contracts.ITenantClient tenantClient) : IUserInvitationManagementService
{
    public async Task<Contracts.Results.GetManyUserInvitationsResult> GetManyAsync(Facade.IdentityManagement.Contracts.Parameters.UserInvitation.GetManyUserInvitationsParameters parameters)
    {
        var result = await identityClient.UserInvitationResource.GetManyAsync(parameters.ToCoreParameters());

        var enrichedInvitations = await EnrichInvitationsWithMembershipStatus(result.Invitations);

        return new Contracts.Results.GetManyUserInvitationsResult
        {
            Invitations = enrichedInvitations
        };
    }
    
    public async Task<UserInvitationDto> CreateAsync(Facade.IdentityManagement.Contracts.Parameters.UserInvitation.CreateUserInvitationParameters parameters)
    {
        var result = await identityClient.UserInvitationResource.CreateAsync(parameters.ToCoreParameters());

        return result.ToFacadeDto();
    }
    
    public async Task AcceptAsync(Facade.IdentityManagement.Contracts.Parameters.UserInvitation.AcceptUserInvitationParameters parameters)
    {
        await identityClient.UserInvitationResource.AcceptAsync(parameters.ToCoreParameters());
    }
    
    public async Task ResendAsync(Facade.IdentityManagement.Contracts.Parameters.UserInvitation.ResendUserInvitationParameters parameters)
    {
        await identityClient.UserInvitationResource.ResendAsync(parameters.ToCoreParameters());
    }
    
    public async Task<Contracts.Results.ValidateUserInvitationResult> ValidateAsync(Facade.IdentityManagement.Contracts.Parameters.UserInvitation.ValidateUserInvitationParameters parameters)
    {
        var result = await identityClient.UserInvitationResource.ValidateAsync(parameters.ToCoreParameters());

        return new Contracts.Results.ValidateUserInvitationResult
        {
            IsValid = result.IsValid,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            TenantMemberRole = result.TenantMemberRole
        };
    }
    
    public async Task DeleteAsync(Facade.IdentityManagement.Contracts.Parameters.UserInvitation.DeleteUserInvitationParameters parameters)
    {
        await identityClient.UserInvitationResource.DeleteAsync(parameters.ToCoreParameters());
    }

    private async Task<List<UserInvitationDto>> EnrichInvitationsWithMembershipStatus(
        IReadOnlyList<Identity.Contracts.Dtos.UserInvitationDto> invitations)
    {
        var acceptedInvitations = invitations.Where(i => i.AcceptedAt.HasValue).ToList();
        var pendingInvitations = invitations.Where(i => !i.AcceptedAt.HasValue).ToList();

        var enrichedInvitations = new List<UserInvitationDto>();

        foreach (var invitation in acceptedInvitations)
        {
            var facadeDto = invitation.ToFacadeDto();
            
            var user = await identityClient.UserResource.GetByEmailAsync(
                new Identity.Contracts.Parameters.User.GetUserByEmailParameters 
                { 
                    Email = invitation.Email 
                });

            if (user != null)
            {
                try
                {
                    await tenantClient.TenantMemberResource.GetAsync(
                        new Tenant.Contracts.Parameters.GetTenantMemberParameters
                        {
                            UserId = user.Id,
                            TenantId = invitation.TenantId
                        });

                    facadeDto.IsMembershipCreated = true;
                }
                catch (Tenant.Contracts.Exceptions.TenantMemberNotFoundException)
                {
                    facadeDto.IsMembershipCreated = false;
                }
            }

            enrichedInvitations.Add(facadeDto);
        }

        enrichedInvitations.AddRange(pendingInvitations.Select(i => i.ToFacadeDto()));

        return enrichedInvitations;
    }
}
