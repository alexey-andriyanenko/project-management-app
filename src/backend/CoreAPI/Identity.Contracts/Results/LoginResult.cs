namespace Identity.Contracts.Results;

public class LoginResult
{
    public Guid UserId { get; set; }
    
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }
}