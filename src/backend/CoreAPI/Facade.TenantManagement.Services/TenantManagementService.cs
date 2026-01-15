using Facade.TenantManagement.Contracts.Dtos;
using Facade.TenantManagement.Contracts.Parameters;
using Facade.TenantManagement.Contracts.Results;
using Facade.TenantManagement.Contracts.Services;
using Facade.TenantManagement.Services.Mappings;

namespace Facade.TenantManagement.Services;

public class TenantManagementService(Tenant.Client.Contracts.ITenantClient tenantClient) : ITenantManagementService
{
    public async Task<TenantDto> GetAsync(GetTenantBySlugParameters parameters)
    {
        var result = await tenantClient.TenantResource.GetAsync(parameters.ToCoreParameters());
        return result.ToFacadeDto();
    }

    public async Task<GetManyTenantsByUserIdResult> GetManyAsync(GetManyTenantsByUserIdParameters parameters)
    {
        var result = await tenantClient.TenantResource.GetManyAsync(parameters.ToCoreParameters());
        return new GetManyTenantsByUserIdResult()
        {
            Tenants = result.Tenants.Select(t => t.ToFacadeDto()).ToList()
        };
    }
    
    public async Task<TenantDto> CreateAsync(CreateTenantParameters parameters)
    {
        var result = await tenantClient.TenantResource.CreateAsync(parameters.ToCoreParameters());
        return result.ToFacadeDto();
    }
    
    public async Task<TenantDto> UpdateAsync(UpdateTenantParameters parameters)
    {
        var result = await tenantClient.TenantResource.UpdateAsync(parameters.ToCoreParameters());
        return result.ToFacadeDto();
    }
}