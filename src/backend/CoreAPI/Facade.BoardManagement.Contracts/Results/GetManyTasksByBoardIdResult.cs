using Facade.BoardManagement.Contracts.Dtos;

namespace Facade.BoardManagement.Contracts.Results;

public class GetManyTasksByBoardIdResult
{
    public IReadOnlyList<TaskDto> Tasks { get; set; } = [];
}