using Facade.TenantManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.TenantManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/members")]
public class TenantMemberManagementController(ITenantMemberManagementService tenantMemberManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<Contracts.Results.GetManyTenantMembersByTenantIdResult> GetManyAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] Contracts.Parameters.GetManyTenantMembersByTenantIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await tenantMemberManagementService.GetManyAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.TenantMemberDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.CreateTenantMemberParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await tenantMemberManagementService.CreateAsync(parameters);
    }

    [HttpPut("{memberUserId}")]
    public async Task<Contracts.Dtos.TenantMemberDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid memberUserId,
        [FromBody] Contracts.Parameters.UpdateTenantMemberParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.MemberUserId = memberUserId;
        return await tenantMemberManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{memberUserId}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid memberUserId,
        [FromBody] Contracts.Parameters.DeleteTenantMemberParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.MemberUserId = memberUserId;
        await tenantMemberManagementService.DeleteAsync(parameters);
    }
}