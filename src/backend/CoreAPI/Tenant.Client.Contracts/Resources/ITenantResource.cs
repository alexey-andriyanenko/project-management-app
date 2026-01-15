using Tenant.Contracts.Dtos;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Results;

namespace Tenant.Client.Contracts.Resources;

public interface ITenantResource
{
    public Task<TenantDto> GetAsync(GetTenantBySlugParameters parameters);
    
    public Task<GetManyTenantsByUserIdResult> GetManyAsync(GetManyTenantsByUserIdParameters parameters);

    public Task<TenantDto> CreateAsync(CreateTenantParameters parameters);
    
    public Task<TenantDto> UpdateAsync(UpdateTenantParameters parameters);
}