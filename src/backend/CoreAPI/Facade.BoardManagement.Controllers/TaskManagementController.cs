using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.BoardManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/tasks")]
public class TaskManagementController(ITaskManagementService taskManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<GetManyTasksByBoardIdResult> GetManyByBoardIdAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] Contracts.Parameters.Task.GetManyTasksByBoardIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await taskManagementService.GetManyAsync(parameters);
    }

    [HttpGet("{taskId}")]
    public async Task<Contracts.Dtos.TaskDto> GetByIdAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] Contracts.Parameters.Task.GetTaskByIdParameters parameters) 
    {
        parameters.TenantId = tenantId;
        return await taskManagementService.GetByIdAsync(parameters);
    }
    
    [HttpPost]
    public async Task<Contracts.Dtos.TaskDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.Task.CreateTaskParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await taskManagementService.CreateAsync(parameters);
    }

    [HttpPut("{taskId}")]
    public async Task<Contracts.Dtos.TaskDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.Task.UpdateTaskParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await taskManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{taskId}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tenantId,
        [FromBody] Contracts.Parameters.Task.DeleteTaskParameters parameters)
    {
        parameters.TenantId = tenantId;
        await taskManagementService.DeleteAsync(parameters);
    }
}