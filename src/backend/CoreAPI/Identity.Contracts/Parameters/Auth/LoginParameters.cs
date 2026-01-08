namespace Identity.Contracts.Parameters.Auth;

public class LoginParameters
{
    public required string Email { get; set; }
    
    public required string Password { get; set; }
}
