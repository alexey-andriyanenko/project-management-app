namespace Facade.TenantManagement.Contracts.Parameters;

public class UpdateTenantParameters
{
    public Guid TenantId { get; set; }
    
    public Guid UserId { get; set; }
    
    public required string Name { get; set; }
}