namespace Tenant.Contracts.Parameters;

public class CreateTenantParameters
{
    public Guid UserId { get; set; }
    
    public required string Name { get; set; }
}
