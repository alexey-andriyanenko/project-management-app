namespace Identity.Contracts.Results;

public class GenerateTokensResult
{
    public required string AccessToken { get; set; }
    
    public required string RefreshToken { get; set; }
}