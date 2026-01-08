using Facade.BoardManagement.Contracts.Parameters.Board;
using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Facade.BoardManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/boards")]
public class BoardManagementControllers(IBoardManagementService boardManagementService)
{
    [HttpGet]
    public async Task<GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] GetManyBoardsByProjectIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await boardManagementService.GetManyByProjectIdAsync(parameters);
    }

    [HttpGet("{boardId}")]
    public async Task<Contracts.Dtos.BoardDto> GetByIdAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] GetBoardByIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await boardManagementService.GetByIdAsync(parameters);
    }

    [HttpPost]
    public async Task<Contracts.Dtos.BoardDto> CreateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] CreateBoardParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await boardManagementService.CreateAsync(parameters);
    }

    [HttpPut("{boardId}")]
    public async Task<Contracts.Dtos.BoardDto> UpdateAsync(
        [FromRoute] Guid tenantId,
        [FromBody] UpdateBoardParameters parameters)
    {
        parameters.TenantId = tenantId;
        return await boardManagementService.UpdateAsync(parameters);
    }

    [HttpDelete("{boardId}")]
    public async Task DeleteAsync(
        [FromRoute] Guid tenantId,
        [FromBody] DeleteBoardParameters parameters)
    {
        parameters.TenantId = tenantId;
        await boardManagementService.DeleteAsync(parameters);
    }
}