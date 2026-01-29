using Tenant.Client.Contracts.Resources;
using Tenant.Contracts.Dtos;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;
using Tenant.Contracts.Services;

namespace Tenant.Client.Resources;

public class TenantResource(ITenantService tenantService) : ITenantResource
{
    public Task<TenantDto> GetAsync(GetTenantBySlugParameters parameters)
        => tenantService.GetAsync(parameters);
    
    public Task<GetManyTenantsByUserIdResult> GetManyAsync(GetManyTenantsByUserIdParameters parameters)
        => tenantService.GetManyAsync(parameters);
    
    public Task<TenantDto> CreateAsync(CreateTenantParameters parameters)
        => tenantService.CreateAsync(parameters);
    
    public Task<TenantDto> UpdateAsync(UpdateTenantParameters parameters)
        => tenantService.UpdateAsync(parameters);
    
    public Task DeleteAsync(DeleteTenantParameters parameters)
        => tenantService.DeleteAsync(parameters);
}