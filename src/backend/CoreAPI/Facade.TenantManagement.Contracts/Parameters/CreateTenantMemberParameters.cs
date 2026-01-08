using Facade.TenantManagement.Contracts.Dtos;

namespace Facade.TenantManagement.Contracts.Parameters;

public class CreateTenantMemberParameters
{
    public Guid TenantId { get; set; }
    
    public Guid MemberUserId { get; set; }
    
    public TenantMemberRole Role { get; set; }
}