namespace Board.DataAccess.Entities;

public class BoardEntity
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public Guid BoardTypeId { get; set; }
    
    public BoardTypeEntity? BoardType { get; set; }

    public ICollection<TaskEntity> Tasks { get; set; } = [];
    
    public ICollection<BoardColumnEntity> Columns { get; set; } = [];
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}