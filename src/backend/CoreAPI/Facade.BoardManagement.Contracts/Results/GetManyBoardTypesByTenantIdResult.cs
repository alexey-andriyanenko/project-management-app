using Facade.BoardManagement.Contracts.Dtos;

namespace Facade.BoardManagement.Contracts.Results;

public class GetManyBoardTypesByTenantIdResult
{
    public IReadOnlyList<BoardTypeDto> BoardTypes { get; set; } = [];
}