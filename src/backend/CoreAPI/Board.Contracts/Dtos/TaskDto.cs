namespace Board.Contracts.Dtos;

public class TaskDto
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid BoardId { get; set; }
    
    public Guid BoardColumnId { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public Guid? AssignedToUserId { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public IReadOnlyList<Guid> TagIds { get; set; } = [];
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}