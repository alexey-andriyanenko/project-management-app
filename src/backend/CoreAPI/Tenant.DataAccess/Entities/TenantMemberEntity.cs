using Tenant.DataAccess.Enums;

namespace Tenant.DataAccess.Entities;

public class TenantMemberEntity
{
    public required Guid TenantId { get; set; }

    public required Guid UserId { get; set; }
    
    public TenantMemberRole Role { get; set; }
}
