using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Board;

namespace Board.Client.Contracts.Resources;

public interface IBoardColumnResource
{
    public Task<BoardColumnDto> GetAsync(GetBoardColumnByIdParameters parameters);
}
