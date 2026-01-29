using Facade.ProjectManagement.Contracts.Results;
using Facade.ProjectManagement.Contracts.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.ClaimsPrincipal.Extensions;

namespace Facade.ProjectManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/projects")]
public class ProjectManagementController(IProjectManagementService projectManagementService) : ControllerBase
{
    [HttpGet("by-slug")]
    public async Task<Contracts.Dtos.ProjectDto> GetBySlugAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] string slug
    )
    {
        var parameters = new Contracts.Parameters.Project.GetProjectBySlugParameters
        {
            MemberId = User.GetUserId(),
            TenantId = tenantId,
            Slug = slug
        };
        return await projectManagementService.GetAsync(parameters);
    }
    
    [HttpGet]
    public async Task<GetManyProjectsByTenantIdResult> GetManyAsync([FromRoute] Guid tenantId)
    {
        return await projectManagementService.GetManyAsync(new Contracts.Parameters.Project.GetManyProjectsByTenantIdParameters
        {
            UserId = User.GetUserId(),
            TenantId = tenantId
        });
    }

    [HttpGet("{id}")]
    public async Task<Contracts.Dtos.ProjectDto> GetAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid id
    )
    {
        return await projectManagementService.GetAsync(new Contracts.Parameters.Project.GetProjectByIdParameters
        {
            UserId = User.GetUserId(),
            TenantId = tenantId,
            ProjectId = id
        });
    }

    [HttpPost]
    public async Task<Contracts.Dtos.ProjectDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.Project.CreateProjectParameters parameters
    )
    {
        parameters.UserId = User.GetUserId();
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
        parameters.UserId = User.GetUserId();
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
        await projectManagementService.DeleteAsync(new Contracts.Parameters.Project.DeleteProjectParameters
        {
            UserId = User.GetUserId(),
            TenantId = tenantId,
            ProjectId = id
        });
    }
}