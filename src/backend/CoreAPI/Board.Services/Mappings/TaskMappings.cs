using Board.Contracts.Dtos;
using Board.DataAccess.Entities;

namespace Board.Services.Mappings;

public static class TaskMappings
{
    public static TaskDto ToDto(this TaskEntity taskEntity)
    {
        return new TaskDto()
        {
            Id = taskEntity.Id,
            TenantId = taskEntity.TenantId,
            ProjectId = taskEntity.ProjectId,
            BoardId = taskEntity.BoardId,
            BoardColumnId = taskEntity.BoardColumnId,
            CreatedByUserId = taskEntity.CreatedByUserId,
            AssignedToUserId = taskEntity.AssigneeUserId,
            Title = taskEntity.Title,
            Description = taskEntity.DescriptionAsJson,
            TagIds = taskEntity.Tags.Select(t => t.TagId).ToList(),
            CreatedAt = taskEntity.CreatedAt,
            UpdatedAt = taskEntity.UpdatedAt
        };
    }
}