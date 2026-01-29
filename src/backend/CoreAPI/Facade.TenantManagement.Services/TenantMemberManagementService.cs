using Facade.TenantManagement.Contracts.Dtos;
using Facade.TenantManagement.Contracts.Parameters;
using Facade.TenantManagement.Contracts.Results;
using Facade.TenantManagement.Contracts.Services;
using Facade.TenantManagement.Services.Mappings;

namespace Facade.TenantManagement.Services;

public class TenantMemberManagementService(
    Tenant.Client.Contracts.ITenantClient tenantClient,
    Identity.Client.Contracts.IIdentityClient identityClient) : ITenantMemberManagementService
{
    public async Task<GetManyTenantMembersByTenantIdResult> GetManyAsync(
        GetManyTenantMembersByTenantIdParameters parameters)
    {
        var members = await tenantClient.TenantMemberResource.GetManyAsync(parameters.ToCoreParameters());
        var enrichedMembers = await EnrichTenantMembers(members.TenantMembers);

        return new GetManyTenantMembersByTenantIdResult()
        {
            TenantMembers = enrichedMembers
        };
    }

    public async Task<TenantMemberDto> CreateAsync(CreateTenantMemberParameters parameters)
    {
        var user = await identityClient.UserResource.CreateAsync(
            new Identity.Contracts.Parameters.User.CreateUserParameters()
            {
                FirstName = parameters.FirstName,
                LastName = parameters.LastName,
                Email = parameters.Email,
                UserName = parameters.UserName,
                Password = parameters.Password
            });

        var createMemberParameters = parameters.ToCoreParameters();
        createMemberParameters.UserId = user.Id;
        
        var member = await tenantClient.TenantMemberResource.CreateAsync(parameters.ToCoreParameters());
        var enrichedMembers = await EnrichTenantMembers(new List<Tenant.Contracts.Dtos.TenantMemberDto>() { member });

        return enrichedMembers.First();
    }

    public async Task<TenantMemberDto> UpdateAsync(UpdateTenantMemberParameters parameters)
    {
        var member = await tenantClient.TenantMemberResource.UpdateAsync(parameters.ToCoreParameters());
        var enrichedMembers = await EnrichTenantMembers(new List<Tenant.Contracts.Dtos.TenantMemberDto>() { member });

        return enrichedMembers.First();
    }

    public async Task DeleteAsync(DeleteTenantMemberParameters parameters)
    {
        await tenantClient.TenantMemberResource.DeleteAsync(parameters.ToCoreParameters());
    }

    public async Task<TenantMemberDto> RetryTenantMemberCreationFromInvitationAsync(RetryTenantMemberCreationFromInvitationParameters parameters)
    {
        var invitation = await identityClient.UserInvitationResource.GetAsync(
            new Identity.Contracts.Parameters.UserInvite.GetUserInvitationParameters
            {
                InvitationId = parameters.InvitationId
            });

        if (!invitation.AcceptedAt.HasValue)
        {
            throw new InvalidOperationException("Invitation has not been accepted yet");
        }

        var user = await identityClient.UserResource.GetByEmailAsync(
            new Identity.Contracts.Parameters.User.GetUserByEmailParameters 
            { 
                Email = invitation.Email 
            });

        if (user == null)
        {
            throw new InvalidOperationException($"User with email {invitation.Email} not found");
        }

        try
        {
            await tenantClient.TenantMemberResource.GetAsync(
                new Tenant.Contracts.Parameters.GetTenantMemberParameters
                {
                    UserId = user.Id,
                    TenantId = invitation.TenantId
                });

            throw new InvalidOperationException("Membership has already been created");
        }
        catch (Tenant.Contracts.Exceptions.TenantMemberNotFoundException)
        {
            // Expected - membership doesn't exist yet, proceed with creation
        }

        var member = await tenantClient.TenantMemberResource.CreateAsync(
            new Tenant.Contracts.Parameters.CreateTenantMemberParameters
            {
                TenantId = invitation.TenantId,
                UserId = user.Id,
                Role = Enum.Parse<Tenant.Contracts.Dtos.TenantMemberRole>(invitation.TenantMemberRole)
            });

        var enrichedMembers = await EnrichTenantMembers(new List<Tenant.Contracts.Dtos.TenantMemberDto>() { member });

        return enrichedMembers.First();
    }
    
    private async Task<IReadOnlyList<TenantMemberDto>> EnrichTenantMembers(
        IReadOnlyList<Tenant.Contracts.Dtos.TenantMemberDto> tenantMembers)
    {
        var userIds = tenantMembers.Select(tm => tm.UserId).ToList();
        var users = await identityClient.UserResource.GetManyAsync(
            new Identity.Contracts.Parameters.User.GetManyUsersByIdsParameters()
            {
                Ids = userIds
            });
        var usersById = users.Users.ToDictionary(u => u.Id, u => u);

        return tenantMembers.Select(tm =>
        {
            var user = usersById[tm.UserId];
            return tm.ToFacadeDto(user);
        }).ToList();
    }
}