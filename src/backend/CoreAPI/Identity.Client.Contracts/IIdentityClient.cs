using Identity.Client.Contracts.Resources;

namespace Identity.Client.Contracts;

public interface IIdentityClient
{
    public IAuthResource AuthResource { get; }
    
    public IUserResource UserResource { get; }
    
    public IUserInvitationResource UserInvitationResource { get; }
}