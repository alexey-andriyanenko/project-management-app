namespace Facade.IdentityManagement.Contracts.Results;

public class RegisterResult
{
    public Guid UserId { get; set; }
    
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }
}