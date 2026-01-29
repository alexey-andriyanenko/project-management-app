namespace Tenant.Contracts.Parameters;

public class GetTenantMemberParameters
{
    public Guid TenantId { get; set; }
    
    public Guid UserId { get; set; }
}
