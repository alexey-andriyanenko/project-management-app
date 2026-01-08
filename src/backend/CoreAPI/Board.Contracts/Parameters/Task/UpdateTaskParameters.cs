namespace Board.Contracts.Parameters.Task;

public class UpdateTaskParameters
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid BoardId { get; set; }
    
    public Guid BoardColumnId { get; set; }
    
    public Guid? AssigneeUserId { get; set; }
    
    public required string Title { get; set; } = string.Empty;
    
    public string? DescriptionAsJson { get; set; }
    
    public string? DescriptionAsPlainText { get; set; }
    
    public IReadOnlyList<Guid> TagIds { get; set; } = [];
}