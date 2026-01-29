using Board.Contracts.Exceptions;
using Board.Contracts.Parameters.Board;
using Board.Contracts.Results;
using Board.Contracts.Services;
using Board.DataAccess;
using Board.DataAccess.Constants;
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
            .FirstOrDefaultAsync(b => b.TenantId == parameters.TenantId && b.ProjectId == parameters.ProjectId && b.Id == parameters.Id);

        if (board == null)
        {
            throw new BoardNotFoundException(tenantId: parameters.TenantId, projectId: parameters.ProjectId, boardId: parameters.Id);
        }
        
        var dto = board.ToDto();
        dto.Columns = dto.Columns
            .OrderBy(c => c.Order)
            .ToList();
        
        return dto;
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
            Boards = boards.Select(x =>
            {
                var dto = x.ToDto();
                dto.Columns = dto.Columns
                    .OrderBy(c => c.Order)
                    .ToList();
                
                return dto;
            }).ToList()
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
        
        var boardType = await dbContext.BoardTypes
            .FirstOrDefaultAsync(bt => bt.Id == parameters.BoardTypeId && bt.TenantId == parameters.TenantId);

        var defaultColumns = GetDefaultColumnsForBoardType(boardType?.Name);
        
        var duplicateColumn = parameters.Columns
            .FirstOrDefault(c => defaultColumns.Any(dc => dc.Equals(c.Name, StringComparison.OrdinalIgnoreCase)));
        
        if (duplicateColumn != null)
        {
            throw new DuplicateColumnNamesException(duplicateColumn.Name);
        }
        
        var allColumnNames = defaultColumns.Concat(parameters.Columns.Select(c => c.Name)).ToList();
        
        var columns = new List<BoardColumnEntity>();
        
        for (var i = 0; i < allColumnNames.Count; i++)
        {
            var column = new BoardColumnEntity()
            {
                Id = Guid.NewGuid(),
                BoardId = board.Id,
                Name = allColumnNames[i],
                Order = i + 1,
                CreatedAt = DateTime.UtcNow,
                CreatedByUserId = parameters.UserId,
            };
            
            columns.Add(column);
        }
        
        board.Columns = columns;
        
        dbContext.Boards.Add(board);
        await dbContext.SaveChangesAsync();
        
        var createdBoard = await dbContext.Boards
            .Include(x => x.Columns)
            .Include(x => x.BoardType)
            .FirstOrDefaultAsync(b => b.Id == board.Id);
        
        if (createdBoard == null)
        {
            throw new BoardNotFoundException(tenantId: parameters.TenantId, projectId: parameters.ProjectId, boardId: board.Id);
        }
        
        var dto = createdBoard.ToDto();
        dto.Columns = dto.Columns
            .OrderBy(c => c.Order)
            .ToList();
        
        return dto;
    }
    
    public async Task<Board.Contracts.Dtos.BoardDto> UpdateAsync(Board.Contracts.Parameters.Board.UpdateBoardParameters parameters)
    {
        var board = await dbContext.Boards
            .Include(x => x.Columns)
            .Include(x => x.BoardType)
            .FirstOrDefaultAsync(b => b.TenantId == parameters.TenantId && b.ProjectId == parameters.ProjectId && b.Id == parameters.Id);
        
        if (board == null)
        {
            throw new BoardNotFoundException(tenantId: parameters.TenantId, projectId: parameters.ProjectId, boardId: parameters.Id);
        }
        
        if (board.Name != parameters.Name) 
        {
            board.Name = parameters.Name;
            board.UpdatedAt = DateTime.UtcNow;
        }
        
        await dbContext.SaveChangesAsync();
        
        var dto = board.ToDto();
        dto.Columns = dto.Columns
            .OrderBy(c => c.Order)
            .ToList();
        
        return dto;
    }
    
    public async Task DeleteAsync(Board.Contracts.Parameters.Board.DeleteBoardParameters parameters)
    {
        var board = dbContext.Boards
            .FirstOrDefault(b => b.TenantId == parameters.TenantId &&  b.ProjectId == parameters.ProjectId && b.Id == parameters.Id);
        
        if (board == null)
        {
            throw new BoardNotFoundException(tenantId: parameters.TenantId, projectId: parameters.ProjectId, boardId: parameters.Id);
        } 
        
        dbContext.Boards.Remove(board);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteManyAsync(DeleteManyBoardsByTenantId parameters, CancellationToken cancellationToken = default)
    {
        await dbContext.Boards
            .Where(b => b.TenantId == parameters.TenantId)
            .ExecuteDeleteAsync(cancellationToken);
    }
    
    public async Task DeleteManyAsync(DeleteManyBoardsByProjectId parameters, CancellationToken cancellationToken = default)
    {
        await dbContext.Boards
            .Where(b => b.TenantId == parameters.TenantId && b.ProjectId == parameters.ProjectId)
            .ExecuteDeleteAsync(cancellationToken);
    }
    
    public async Task SeedBoardTypesForTenantAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        var existingBoardTypes = await dbContext.BoardTypes
            .Where(bt => bt.TenantId == tenantId)
            .ToListAsync(cancellationToken);
        
        if (existingBoardTypes.Any())
        {
            return;
        }
        
        var defaultBoardTypes = new List<BoardTypeEntity>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = BoardTypeNames.Kanban,
                IsEssential = true,
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = BoardTypeNames.Scrum,
                IsEssential = true,
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = BoardTypeNames.Backlog,
                IsEssential = true,
            },
            new()
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Name = BoardTypeNames.Custom,
                IsEssential = false,
            },
        };
        
        dbContext.BoardTypes.AddRange(defaultBoardTypes);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
    private List<string> GetDefaultColumnsForBoardType(string? boardTypeName)
    {
        return boardTypeName switch
        {
            BoardTypeNames.Scrum => new List<string> { "To Do", "In Progress", "In Review", "Done" },
            BoardTypeNames.Kanban => new List<string> { "Backlog", "To Do", "In Progress", "Done" },
            _ => new List<string>()
        };
    }
}