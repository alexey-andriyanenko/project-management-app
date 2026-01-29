using Tenant.Contracts.Dtos;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;

namespace Tenant.Client.Contracts.Resources;

public interface ITenantMemberResource
{
    public Task<GetManyTenantMembersByTenantIdResult> GetManyAsync(GetManyTenantMembersByTenantIdParameters parameters);
    
    public Task<TenantMemberDto> GetAsync(GetTenantMemberParameters parameters);
    
    public Task<TenantMemberDto> CreateAsync(CreateTenantMemberParameters parameters);
    
    public Task<TenantMemberDto> UpdateAsync(UpdateTenantMemberParameters parameters);
    
    public Task DeleteAsync(DeleteTenantMemberParameters parameters);
}