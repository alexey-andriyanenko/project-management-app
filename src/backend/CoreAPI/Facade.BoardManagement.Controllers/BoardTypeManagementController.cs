using Facade.BoardManagement.Contracts.Parameters.BoardType;
using Facade.BoardManagement.Contracts.Results;
using Facade.BoardManagement.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.ClaimsPrincipal.Extensions;

namespace Facade.BoardManagement.Controllers;

[ApiController]
[Route("api/v1/tenants/{tenantId}/board-types")]
public class BoardTypeManagementController(IBoardTypeManagementService boardTypeManagementService) : ControllerBase
{
    [HttpGet]
    public async Task<GetManyBoardTypesByTenantIdResult> GetManyAsync(
        [FromRoute] Guid tenantId,
        [FromQuery] GetManyBoardTypesByTenantIdParameters parameters)
    {
        parameters.TenantId = tenantId;
        parameters.UserId = User.GetUserId();
        return await boardTypeManagementService.GetManyAsync(parameters);
    }
}
