namespace Facade.BoardManagement.Contracts.Dtos;

public class BoardTypeDto
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid? ProjectId { get; set; }
    
    public bool IsEssential { get; set; }
    
    public required string Name { get; set; }
}