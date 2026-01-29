using Facade.BoardManagement.Contracts.Parameters.BoardType;
using Facade.BoardManagement.Contracts.Results;

namespace Facade.BoardManagement.Contracts.Services;

public interface IBoardTypeManagementService
{
    public Task<GetManyBoardTypesByTenantIdResult> GetManyAsync(GetManyBoardTypesByTenantIdParameters parameters);
}