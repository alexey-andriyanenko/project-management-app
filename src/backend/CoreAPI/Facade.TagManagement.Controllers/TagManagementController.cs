using Facade.TagManagement.Contracts.Parameters;
using Facade.TagManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.TagManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/tags")]
public class TagManagementController(ITagManagementService tagManagementService) : ControllerBase
{
    public async Task<Contracts.Results.GetManyTagsByTenantIdResult> GetManyByTenantIdAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] Guid? projectId,
        [FromQuery] Contracts.Parameters.GetManyTagsByTenantIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await tagManagementService.GetManyAsync(parameters);
    }

    [HttpGet("by-ids")]
    public async Task<Contracts.Results.GetManyTagsByIdsResult> GetManyByIdsAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] Guid? projectId,
        [FromQuery] Contracts.Parameters.GetManyTagsByIdsParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await tagManagementService.GetManyAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.TagDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] Guid? projectId,
        [FromBody] Contracts.Parameters.CreateTagParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await tagManagementService.CreateAsync(parameters);
    }

    [HttpPut("{tagId}")]
    public async Task<Contracts.Dtos.TagDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] Guid? projectId,
        [FromBody] Contracts.Parameters.UpdateTagParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await tagManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{tagId}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tagId,
        [FromRoute] Guid tenantId,
        [FromQuery] Guid? projectId)
    {
        await tagManagementService.DeleteAsync(new DeleteTagParameters()
        {
            Id = tagId,
            TenantId = tenantId,
            ProjectId = projectId
        });
    }
}