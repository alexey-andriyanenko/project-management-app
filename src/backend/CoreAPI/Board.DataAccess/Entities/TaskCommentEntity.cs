namespace Board.DataAccess.Entities;

public class TaskCommentEntity
{
    public Guid Id { get; set; }
    
    public Guid TaskId { get; set; }
    
    public TaskEntity? Task { get; set; }
    
    public required string ContentAsJson { get; set; }
    
    public required string ContentAsPlainText { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}