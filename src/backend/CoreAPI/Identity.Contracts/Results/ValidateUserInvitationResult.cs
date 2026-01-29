namespace Identity.Contracts.Results;

public class ValidateUserInvitationResult
{
    public bool IsValid { get; set; }
    
    public Guid TenantId { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    
    public string? TenantMemberRole { get; set; }
}