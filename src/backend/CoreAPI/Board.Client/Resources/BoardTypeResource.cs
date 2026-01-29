using Board.Client.Contracts.Resources;
using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;
using Board.Contracts.Services;

namespace Board.Client.Resources;

public class BoardTypeResource(IBoardTypeService boardTypeService) : IBoardTypeResource
{
    public Task<GetManyBoardTypesResult> GetManyAsync(GetManyBoardTypesParameters parameters)
        => boardTypeService.GetManyAsync(parameters);
}