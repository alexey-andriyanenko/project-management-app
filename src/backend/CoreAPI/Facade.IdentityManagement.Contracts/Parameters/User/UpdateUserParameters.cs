namespace Facade.IdentityManagement.Contracts.Parameters.User;

public class UpdateUserParameters
{
    public Guid Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string UserName { get; set; }
    
    public required string Email { get; set; }
}