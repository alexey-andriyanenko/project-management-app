using Facade.TenantManagement.Contracts.Dtos;
using Facade.TenantManagement.Contracts.Parameters;
using Facade.TenantManagement.Contracts.Results;

namespace Facade.TenantManagement.Contracts.Services;

public interface ITenantManagementService
{
    public Task<TenantDto> GetAsync(GetTenantBySlugParameters parameters);
    
    public Task<GetManyTenantsByUserIdResult> GetManyAsync(GetManyTenantsByUserIdParameters parameters);
    
    public Task<TenantDto> CreateAsync(CreateTenantParameters parameters);
    
    public Task<TenantDto> UpdateAsync(UpdateTenantParameters parameters);
}