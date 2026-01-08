using Board.Contracts.Dtos;

namespace Board.Contracts.Results;

public class GetManyTasksByBoardIdResult
{
    public IReadOnlyList<TaskDto> Tasks { get; set; } = [];
}