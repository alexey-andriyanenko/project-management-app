using Tenant.Contracts.Dtos;

namespace Tenant.Contracts.Parameters;

public class CreateTenantMemberParameters
{
    public Guid TenantId { get; set; }
    
    public Guid UserId { get; set; }
    
    public TenantMemberRole Role { get; set; }
}