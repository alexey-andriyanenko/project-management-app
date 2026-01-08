using Board.Contracts.Exceptions;
using Board.Contracts.Results;
using Board.Contracts.Services;
using Board.DataAccess;
using Board.DataAccess.Entities;
using Board.Services.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Board.Services;

public class BoardService(BoardDbContext dbContext) : IBoardService
{
    public async Task<Board.Contracts.Dtos.BoardDto> GetByIdAsync(Board.Contracts.Parameters.Board.GetBoardByIdParameters parameters)
    {
        var board = await dbContext.Boards
            .Include(x => x.Columns)
            .Include(x => x.BoardType)
            .FirstOrDefaultAsync(b => b.TenantId == parameters.TenantId && b.Id == parameters.Id);

        if (board == null)
        {
            throw new BoardNotFoundException(parameters.Id, parameters.TenantId);
        }
        
        return board.ToDto();
    }
    
    public async Task<Board.Contracts.Results.GetManyBoardsByProjectIdResult> GetManyByProjectIdAsync(Board.Contracts.Parameters.Board.GetManyBoardsByProjectIdParameters parameters)
    {
        var boards = await dbContext.Boards
            .Where(b => b.TenantId == parameters.TenantId && b.ProjectId == parameters.ProjectId)
            .Include(x =>x.Columns)
            .Include(x => x.BoardType)
            .ToListAsync();

        return new GetManyBoardsByProjectIdResult()
        {
            Boards = boards.Select(x => x.ToDto()).ToList()
        };
    }
    
    public async Task<Board.Contracts.Dtos.BoardDto> CreateAsync(Board.Contracts.Parameters.Board.CreateBoardParameters parameters)
    {
        var board = new BoardEntity()
        {
            Id = Guid.NewGuid(),
            TenantId = parameters.TenantId,
            ProjectId = parameters.ProjectId,
            Name = parameters.Name,
            BoardTypeId = parameters.BoardTypeId,
            CreatedAt = DateTime.UtcNow,
            CreatedByUserId = parameters.UserId,
        };
        
        var columns = new List<BoardColumnEntity>();

        for (var i = 0; i < parameters.Columns.Count; i++)
        {
            var column = new BoardColumnEntity()
            {
                Id = Guid.NewGuid(),
                BoardId = board.Id,
                Name = parameters.Columns[i].Name,
                Order = i + 1,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = parameters.UserId,
            };
            
            columns.Add(column);
        }
        
        board.Columns = columns;
        
        dbContext.Boards.Add(board);
        await dbContext.SaveChangesAsync();
        
        return board.ToDto();
    }
    
    public async Task<Board.Contracts.Dtos.BoardDto> UpdateAsync(Board.Contracts.Parameters.Board.UpdateBoardParameters parameters)
    {
        var board = await dbContext.Boards
            .Include(x => x.Columns)
            .FirstOrDefaultAsync(b => b.TenantId == parameters.TenantId && b.Id == parameters.TenantId);
        
        if (board == null)
        {
            throw new BoardNotFoundException(parameters.Id, parameters.TenantId);
        }
        
        if (board.Name != parameters.Name) 
        {
            board.Name = parameters.Name;
            board.UpdatedAt = DateTime.UtcNow;
        }
        
        await dbContext.SaveChangesAsync();
        
        return board.ToDto();
    }
    
    public async Task DeleteAsync(Board.Contracts.Parameters.Board.DeleteBoardParameters parameters)
    {
        var board = dbContext.Boards
            .FirstOrDefault(b => b.TenantId == parameters.TenantId && b.Id == parameters.Id);
        
        if (board == null)
        {
            throw new BoardNotFoundException(parameters.Id, parameters.TenantId);
        } 
        
        dbContext.Boards.Remove(board);
        await dbContext.SaveChangesAsync();
    }
}