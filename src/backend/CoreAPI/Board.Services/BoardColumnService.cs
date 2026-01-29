using Board.Contracts.Exceptions;
using Board.Contracts.Services;
using Board.DataAccess;
using Board.Services.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Board.Services;

public class BoardColumnService(BoardDbContext dbContext) : IBoardColumnService
{
    public async Task<Board.Contracts.Dtos.BoardColumnDto> GetAsync(Board.Contracts.Parameters.Board.GetBoardColumnByIdParameters parameters)
    {
        var board = await dbContext.Boards.FirstOrDefaultAsync(x => x.TenantId == parameters.TenantId && x.ProjectId == parameters.ProjectId && x.Id == parameters.BoardId);
        
        if (board == null)
        {
            throw new BoardNotFoundException(tenantId: parameters.TenantId, projectId: parameters.ProjectId, boardId: parameters.BoardId);
        }
        
        var column = await dbContext.BoardColumns
            .FirstOrDefaultAsync(c => c.Id == parameters.BoardColumnId
                                      && c.BoardId == parameters.BoardId);

        if (column == null)
        {
            throw new BoardColumnNotFoundException(tenantId: parameters.TenantId, projectId: parameters.ProjectId, boardId: parameters.BoardId, boardColumnId: parameters.BoardColumnId);
        }
        
        return column.ToDto();
    }
}