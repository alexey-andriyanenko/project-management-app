using Board.Contracts.Dtos;
using Board.Contracts.Parameters.Board;

namespace Board.Contracts.Services;

public interface IBoardColumnService
{
    public Task<BoardColumnDto> GetAsync(GetBoardColumnByIdParameters parameters);
}