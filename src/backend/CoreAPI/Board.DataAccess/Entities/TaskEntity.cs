namespace Board.DataAccess.Entities;

public class TaskEntity
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? DescriptionAsJson { get; set; }
    
    public string? DescriptionAsPlainText { get; set; }

    public Guid TenantId { get; set; }
    public Guid ProjectId { get; set; }
    
    public Guid BoardId { get; set; }
    public BoardEntity? Board { get; set; }
    
    public Guid BoardColumnId { get; set; }
    
    public BoardColumnEntity? BoardColumn { get; set; }

    public Guid CreatedByUserId { get; set; }
    public Guid? AssigneeUserId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public List<TaskCommentEntity> Comments { get; set; } = [];
    public List<TaskTagEntity> Tags { get; set; } = [];
}