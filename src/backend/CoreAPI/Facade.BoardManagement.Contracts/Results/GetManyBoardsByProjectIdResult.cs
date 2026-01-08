using Facade.BoardManagement.Contracts.Dtos;

namespace Facade.BoardManagement.Contracts.Results;

public class GetManyBoardsByProjectIdResult
{
    public IReadOnlyList<BoardDto> Boards { get; set; } = [];
}