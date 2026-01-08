namespace Tenant.Contracts.Parameters;

public class DeleteTenantMemberParameters
{
    public Guid TenantId { get; set; }
    
    public Guid UserId { get; set; }
}