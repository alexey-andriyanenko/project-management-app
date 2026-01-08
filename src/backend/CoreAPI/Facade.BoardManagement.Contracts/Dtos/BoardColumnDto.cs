namespace Facade.BoardManagement.Contracts.Dtos;

public class BoardColumnDto
{
    public Guid Id { get; set; }
    
    public Guid BoardId { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public required string Name { get; set; }
    
    public int Order { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}