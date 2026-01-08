using Board.Contracts.Dtos;
using Board.DataAccess.Entities;

namespace Board.Services.Mappings;

public static class BoardMappings
{
    public static BoardDto ToDto(this BoardEntity entity)
    {
        return new BoardDto()
        {
            Id = entity.Id,
            TenantId = entity.TenantId,
            ProjectId = entity.ProjectId,
            CreatedByUserId = entity.CreatedByUserId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Type = entity.BoardType!.ToDto(),
            Columns = entity.Columns.Select(c => c.ToDto()).ToList(),
        };
    }

    private static BoardColumnDto ToDto(this BoardColumnEntity entity)
    {
        return new BoardColumnDto()
        {
            Id = entity.Id,
            BoardId = entity.BoardId,
            Name = entity.Name,
            Order = entity.Order,
            CreatedByUserId = entity.CreatedByUserId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        };
    }
    
    private static BoardTypeDto ToDto(this BoardTypeEntity entity)
    {
        return new BoardTypeDto()
        {
            Id = entity.Id,
            TenantId = entity.TenantId,
            ProjectId = entity.ProjectId,
            IsEssential = entity.IsEssential,
            Name = entity.Name,
        };
    }
}