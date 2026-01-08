using Facade.ProjectManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.ProjectManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/projects/{projectId}/members")]
public class ProjectMemberManagementController(IProjectMemberManagementService projectMemberManagementService)
{
    [HttpGet]
    public async Task<Contracts.Results.GetManyProjectMembersByProjectIdResult> GetManyAsync([FromRoute] Guid tenantId,
        [FromRoute] Guid projectId)
    {
        var parameters = new Contracts.Parameters.ProjectMember.GetManyProjectMembersByProjectIdParameters
        {
            TenantId = tenantId,
            ProjectId = projectId
        };
        return await projectMemberManagementService.GetManyAsync(parameters);
    }

    [HttpGet("{id}")]
    public async Task<Contracts.Dtos.ProjectMemberDto> GetAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid id
    )
    {
        var parameters = new Contracts.Parameters.ProjectMember.GetProjectMemberByIdParameters
        {
            TenantId = tenantId,
            ProjectId = projectId,
            MemberUserId = id
        };
        return await projectMemberManagementService.GetAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.ProjectMemberDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromBody] Contracts.Parameters.ProjectMember.CreateProjectMemberParameters parameters
    )
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await projectMemberManagementService.CreateAsync(parameters);
    }

    [HttpPut("{id}")]
    public async Task<Contracts.Dtos.ProjectMemberDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid id,
        [FromBody] Contracts.Parameters.ProjectMember.UpdateProjectMemberParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        parameters.MemberUserId = id;
        return await projectMemberManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{id}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid id
    )
    {
        var parameters = new Contracts.Parameters.ProjectMember.DeleteProjectMemberParameters
        {
            TenantId = tenantId,
            ProjectId = projectId,
            MemberUserId = id
        };
        await projectMemberManagementService.DeleteAsync(parameters);
    }
}