namespace Facade.BoardManagement.Contracts.Dtos;

public class TaskTagDto
{
    public Guid TagId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Color { get; set; }
}