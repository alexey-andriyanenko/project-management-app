using Facade.BoardManagement.Contracts.Parameters.Board;
using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.BoardManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/projects/{projectId}/boards")]
public class BoardManagementControllers(IBoardManagementService boardManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromQuery] GetManyBoardsByProjectIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await boardManagementService.GetManyByProjectIdAsync(parameters);
    }

    [HttpGet("{boardId}")]
    public async Task<Contracts.Dtos.BoardDto> GetByIdAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid boardId,
        [FromQuery] GetBoardByIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        parameters.Id = boardId;
        return await boardManagementService.GetByIdAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.BoardDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromBody] CreateBoardParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        return await boardManagementService.CreateAsync(parameters);
    }

    [HttpPut("{boardId}")]
    public async Task<Contracts.Dtos.BoardDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromRoute] Guid boardId,
        [FromBody] UpdateBoardParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        parameters.Id = boardId;
        return await boardManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{boardId}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tenantId,
        [FromRoute] Guid projectId,
        [FromBody] DeleteBoardParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.ProjectId = projectId;
        await boardManagementService.DeleteAsync(parameters);
    }
}