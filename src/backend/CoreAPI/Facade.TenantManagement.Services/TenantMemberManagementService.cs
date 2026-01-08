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