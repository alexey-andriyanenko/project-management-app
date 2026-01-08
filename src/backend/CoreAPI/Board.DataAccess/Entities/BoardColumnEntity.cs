namespace Board.DataAccess.Entities;

public class BoardColumnEntity
{
    public Guid Id { get; set; }
    
    public Guid BoardId { get; set; }
    
    public BoardEntity? Board { get; set; }
    
    public int Order { get; set; }
    
    public required string Name { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}
