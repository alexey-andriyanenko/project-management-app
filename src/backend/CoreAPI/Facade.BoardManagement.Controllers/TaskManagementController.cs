using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.ClaimsPrincipal.Extensions;

namespace Facade.BoardManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/projects/{projectId}/tasks")]
public class TaskManagementController(ITaskManagementService taskManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<GetManyTasksByBoardIdResult> GetManyByBoardIdAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromQuery] Contracts.Parameters.Task.GetManyTasksByBoardIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await taskManagementService.GetManyAsync(parameters);
    }

    [HttpGet("{taskId}")]
    public async Task<Contracts.Dtos.TaskDto> GetByIdAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid taskId,
        [FromQuery] Contracts.Parameters.Task.GetTaskByIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        parameters.Id = taskId;
        return await taskManagementService.GetByIdAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.TaskDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromBody] Contracts.Parameters.Task.CreateTaskParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        parameters.CreatorUserId = User.GetUserId();
        return await taskManagementService.CreateAsync(parameters);
    }

    [HttpPut("{taskId}")]
    public async Task<Contracts.Dtos.TaskDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid taskId,
        [FromBody] Contracts.Parameters.Task.UpdateTaskParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        parameters.Id = taskId;
        return await taskManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{taskId}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid taskId,
        [FromQuery] Guid boardId
    )
    {
        await taskManagementService.DeleteAsync(new()
        {
            TenantId = tenantId,
            ProjectId = projectId,
            BoardId = boardId,
            Id = taskId
        });
    }
}