using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;

namespace Board.Contracts.Services;

public interface IBoardTypeService
{
    public Task<GetManyBoardTypesResult> GetManyAsync(GetManyBoardTypesParameters parameters);
}