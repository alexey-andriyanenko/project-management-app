using Tenant.Contracts.Dtos;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;

namespace Tenant.Contracts.Services;

public interface ITenantService
{
    public Task<TenantDto> GetAsync(GetTenantBySlugParameters parameters);
    
    public Task<GetManyTenantsByUserIdResult> GetManyAsync(GetManyTenantsByUserIdParameters parameters);

    public Task<TenantDto> CreateAsync(CreateTenantParameters parameters);
    
    public Task<TenantDto> UpdateAsync(UpdateTenantParameters parameters);
    
    public Task DeleteAsync(DeleteTenantParameters parameters);
}