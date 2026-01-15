using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;
using Board.Contracts.Services;
using Board.DataAccess;
using Board.Services.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Board.Services;

public class BoardTypeService(BoardDbContext dbContext) : IBoardTypeService
{
    public async Task<GetManyBoardTypesResult> GetManyAsync(GetManyBoardTypesParameters parameters)
    {
        var boardTypes = await dbContext.BoardTypes
            .Where(bt => bt.TenantId == parameters.TenantId)
            .ToListAsync();

        return new GetManyBoardTypesResult
        {
            BoardTypes = boardTypes.Select(x => x.ToDto()).ToList()
        };
    }
}