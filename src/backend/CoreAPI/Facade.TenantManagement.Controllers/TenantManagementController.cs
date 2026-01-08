using Facade.TenantManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.TenantManagement.Controllers;

[ApiController]
[Route("api/v1/tenants")]
public class TenantManagementController(ITenantManagementService tenantManagementService)
{
    [HttpGet]
    public async Task<Contracts.Results.GetManyTenantsByUserIdResult> GetManyByUserIdAsync(
        [FromQuery] Contracts.Parameters.GetManyTenantsByUserIdParameters parameters)
    {
        return await tenantManagementService.GetManyAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.TenantDto> CreateAsync(
        [FromBody] Contracts.Parameters.CreateTenantParameters parameters)
    {
        return await tenantManagementService.CreateAsync(parameters);
    }

    [HttpPut("{tenantId}")]
    public async Task<Contracts.Dtos.TenantDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.UpdateTenantParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await tenantManagementService.UpdateAsync(parameters);
    }
}