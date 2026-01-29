using Tenant.Client.Contracts.Resources;
using Tenant.Contracts.Dtos;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;
using Tenant.Contracts.Services;

namespace Tenant.Client.Resources;

public class TenantMemberResource(ITenantMemberService tenantMemberService) : ITenantMemberResource
{
    public Task<GetManyTenantMembersByTenantIdResult> GetManyAsync(GetManyTenantMembersByTenantIdParameters parameters)
        => tenantMemberService.GetManyAsync(parameters);
    
    public Task<TenantMemberDto> CreateAsync(CreateTenantMemberParameters parameters)
        => tenantMemberService.CreateAsync(parameters);

    public Task<TenantMemberDto> GetAsync(GetTenantMemberParameters parameters)
        => tenantMemberService.GetAsync(parameters);
    
    public Task<TenantMemberDto> UpdateAsync(UpdateTenantMemberParameters parameters)
        => tenantMemberService.UpdateAsync(parameters);
    
    public Task DeleteAsync(DeleteTenantMemberParameters parameters)
        => tenantMemberService.DeleteAsync(parameters);
}