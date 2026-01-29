namespace Facade.TenantManagement.Contracts.Parameters;

public class DeleteTenantParameters
{
    public Guid TenantId { get; set; }
    
    public Guid UserId { get; set; }
}
