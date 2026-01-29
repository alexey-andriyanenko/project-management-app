using Identity.Client.Contracts;
using Identity.Client.Contracts.Resources;

namespace Identity.Client;

public class IdentityClient(
    IAuthResource authResource, 
    IUserResource userResource,
    IUserInvitationResource userInvitationResource) : IIdentityClient
{
    public IAuthResource AuthResource { get; } = authResource;
    
    public IUserResource UserResource { get; } = userResource;
    
    public IUserInvitationResource UserInvitationResource { get; } = userInvitationResource;
}