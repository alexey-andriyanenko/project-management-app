namespace Facade.TagManagement.Contracts.Parameters;

public class CreateTagParameters
{
    public Guid TenantId { get; set; }
    
    public Guid? ProjectId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Color { get; set; }
}