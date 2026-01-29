using Facade.TenantManagement.Contracts.Parameters;
using Facade.TenantManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.ClaimsPrincipal.Extensions;

namespace Facade.TenantManagement.Controllers;

[ApiController]
[Route("api/v1/tenants")]
public class TenantManagementController(ITenantManagementService tenantManagementService) : ControllerBase
{
    [HttpGet("by-slug")]
    public async Task<Contracts.Dtos.TenantDto> GetAsync(
        [FromQuery] Contracts.Parameters.GetTenantBySlugParameters parameters)
    {
        parameters.MemberId = User.GetUserId();
        return await tenantManagementService.GetAsync(parameters);
    }
    
    [HttpGet]
    public async Task<ActionResult<Contracts.Results.GetManyTenantsByUserIdResult>> GetManyByUserIdAsync()
    {
        return await tenantManagementService.GetManyAsync(new GetManyTenantsByUserIdParameters()
        {
            UserId = User.GetUserId()
        });
    }

    [HttpPost]
    public async Task<Contracts.Dtos.TenantDto> CreateAsync(
        [FromBody] Contracts.Parameters.CreateTenantParameters parameters)
    {
        parameters.UserId = User.GetUserId();
        return await tenantManagementService.CreateAsync(parameters);
    }

    [HttpPut("{tenantId}")]
    public async Task<Contracts.Dtos.TenantDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.UpdateTenantParameters parameters)
    {
        parameters.UserId = User.GetUserId();
        parameters.TenantId = tenantId;
        return await tenantManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{tenantId}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid tenantId)
    {
        await tenantManagementService.DeleteAsync(new Contracts.Parameters.DeleteTenantParameters
        {
            TenantId = tenantId,
            UserId = User.GetUserId()
        });
        return NoContent();
    }
}