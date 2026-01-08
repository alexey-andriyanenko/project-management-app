namespace Facade.TenantManagement.Contracts.Dtos;

public class TenantMemberDto
{
    public Guid TenantId { get; set; }
    
    public Guid UserId { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public TenantMemberRole Role { get; set; }
}