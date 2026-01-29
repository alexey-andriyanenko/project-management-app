namespace Facade.BoardManagement.Contracts.Dtos;

public class BoardDto
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    
    public required string Name { get; set; }
    
    public required BoardTypeDto Type { get; set; }
    
    public IReadOnlyList<BoardColumnDto> Columns { get; set; } = [];
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}