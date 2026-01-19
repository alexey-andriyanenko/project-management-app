using Facade.TenantManagement.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Facade.TenantManagement.Controllers;

[ApiController]
[Route("api/v1/tenants")]
public class TenantManagementController(ITenantManagementService tenantManagementService) : ControllerBase
{
    [HttpGet("by-slug")]
    public async Task<Contracts.Dtos.TenantDto> GetAsync(
        [FromQuery] Contracts.Parameters.GetTenantBySlugParameters parameters)
    {
        // parameters.MemberId = httpContextAccessor.HttpContext.User
        return await tenantManagementService.GetAsync(parameters);
    }
    
    [HttpGet("{tenantId}")]
    public async Task<Contracts.Dtos.TenantDto> GetByIdAsync(
        [FromRoute] Guid tenantId)
    {
        return await Task.FromResult(new Contracts.Dtos.TenantDto
        {
            Id = tenantId,
            Name = "Demo Tenant",
            Slug = "demo-tenant",
        });
    }
    
    [HttpGet]
    public async Task<ActionResult<Contracts.Results.GetManyTenantsByUserIdResult>> GetManyByUserIdAsync(
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