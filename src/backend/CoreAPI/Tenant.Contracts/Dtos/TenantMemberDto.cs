namespace Tenant.Contracts.Dtos;

public class TenantMemberDto
{
    public Guid TenantId { get; set; }
    
    public Guid UserId { get; set; }
    
    public TenantMemberRole Role { get; set; }
}