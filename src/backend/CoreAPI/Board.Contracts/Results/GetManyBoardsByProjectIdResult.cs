using Board.Contracts.Dtos;

namespace Board.Contracts.Results;

public class GetManyBoardsByProjectIdResult
{
    public IReadOnlyList<BoardDto> Boards { get; set; } = [];
}