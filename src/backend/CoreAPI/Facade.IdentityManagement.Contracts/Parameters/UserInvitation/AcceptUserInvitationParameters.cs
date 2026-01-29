namespace Facade.IdentityManagement.Contracts.Parameters.UserInvitation;

public class AcceptUserInvitationParameters
{
    public required string InvitationToken { get; set; }
    
    public required string UserName { get; set; }
    
    public required string Password { get; set; }
}
