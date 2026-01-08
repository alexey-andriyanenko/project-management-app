namespace Facade.IdentityManagement.Contracts.Results;

public class RefreshAccessTokenResult
{
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }
}