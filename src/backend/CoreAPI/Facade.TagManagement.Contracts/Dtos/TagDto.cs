namespace Facade.TagManagement.Contracts.Dtos;

public class TagDto
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid? ProjectId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Color { get; set; }
}