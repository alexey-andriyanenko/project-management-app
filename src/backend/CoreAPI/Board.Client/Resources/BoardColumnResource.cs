using Board.Client.Contracts.Resources;
using Board.Contracts.Services;

namespace Board.Client.Resources;

public class BoardColumnResource(IBoardColumnService boardColumnService) : IBoardColumnResource
{
    public Task<Board.Contracts.Dtos.BoardColumnDto> GetAsync(Board.Contracts.Parameters.Board.GetBoardColumnByIdParameters parameters)
        => boardColumnService.GetAsync(parameters);
}