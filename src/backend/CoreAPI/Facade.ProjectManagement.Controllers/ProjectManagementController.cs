using Facade.ProjectManagement.Contracts.Results;
using Facade.ProjectManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.ProjectManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/projects")]
public class ProjectManagementController(IProjectManagementService projectManagementService)
{
    [HttpGet("by-slug")]
    public async Task<Contracts.Dtos.ProjectDto> GetBySlugAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] string slug
    )
    {
        var parameters = new Contracts.Parameters.Project.GetProjectBySlugParameters
        {
            MemberId = Guid.Empty, // To be replaced with actual member ID from context
            TenantId = tenantId,
            Slug = slug
        };
        return await projectManagementService.GetAsync(parameters);
    }
    
    [HttpGet]
    public async Task<GetManyProjectsByTenantIdResult> GetManyAsync([FromRoute] Guid tenantId)
    {
        var parameters = new Contracts.Parameters.Project.GetManyProjectsByTenantIdParameters
        {
            TenantId = tenantId
        };
        return await projectManagementService.GetManyAsync(parameters);
    }

    [HttpGet("{id}")]
    public async Task<Contracts.Dtos.ProjectDto> GetAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid id
    )
    {
        var parameters = new Contracts.Parameters.Project.GetProjectByIdParameters
        {
            TenantId = tenantId,
            ProjectId = id
        };
        return await projectManagementService.GetAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.ProjectDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.Project.CreateProjectParameters parameters
    )
    {
        parameters.TenantId = tenantId;
        return await projectManagementService.CreateAsync(parameters);
    }

    [HttpPut("{id}")]
    public async Task<Contracts.Dtos.ProjectDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid id,
        [FromBody] Contracts.Parameters.Project.UpdateProjectParameters parameters
    )
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = id;
        return await projectManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid id
    )
    {
        var parameters = new Contracts.Parameters.Project.DeleteProjectParameters
        {
            TenantId = tenantId,
            ProjectId = id
        };
        await projectManagementService.DeleteAsync(parameters);
    }
}