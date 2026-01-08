namespace Facade.TenantManagement.Contracts.Parameters;

public class DeleteTenantMemberParameters
{
    public Guid TenantId { get; set; }
    
    public Guid MemberUserId { get; set; }
}