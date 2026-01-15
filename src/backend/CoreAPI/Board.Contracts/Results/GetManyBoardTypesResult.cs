using Board.Contracts.Dtos;

namespace Board.Contracts.Results;

public class GetManyBoardTypesResult
{
    public IReadOnlyList<BoardTypeDto> BoardTypes { get; set; } = [];
}