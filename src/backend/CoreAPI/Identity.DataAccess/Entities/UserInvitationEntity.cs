using Identity.DataAccess.Enums;

namespace Identity.DataAccess.Entities;

public class UserInvitationEntity
{
    public Guid Id { get; set; }
    
    public Guid TenantId { get; set; }
    
    public required string Token { get; set; }
    
    public required string Email { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required TenantMemberRole TenantMemberRole { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ExpiresAt { get; set; }
    
    public DateTime? AcceptedAt { get; set; }
}
