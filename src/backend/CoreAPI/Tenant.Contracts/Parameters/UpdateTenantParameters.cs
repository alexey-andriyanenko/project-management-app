namespace Tenant.Contracts.Parameters;

public class UpdateTenantParameters
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
}