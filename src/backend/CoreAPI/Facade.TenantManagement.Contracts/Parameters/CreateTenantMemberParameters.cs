using Facade.TenantManagement.Contracts.Dtos;

namespace Facade.TenantManagement.Contracts.Parameters;

public class CreateTenantMemberParameters
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Password { get; set; }
    
    public required string Email { get; set; }
    
    public required string UserName { get; set; }
    
    public Guid TenantId { get; set; }
    
    public TenantMemberRole Role { get; set; }
}