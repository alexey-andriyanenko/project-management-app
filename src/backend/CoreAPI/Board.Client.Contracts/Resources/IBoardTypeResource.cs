using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;

namespace Board.Client.Contracts.Resources;

public interface IBoardTypeResource
{
    public Task<GetManyBoardTypesResult> GetManyAsync(GetManyBoardTypesParameters parameters);
}