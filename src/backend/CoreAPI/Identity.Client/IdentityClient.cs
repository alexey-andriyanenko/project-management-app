using Identity.Client.Contracts;
using Identity.Client.Contracts.Resources;

namespace Identity.Client;

public class IdentityClient(IAuthResource authResource, IUserResource userResource) : IIdentityClient
{
    public IAuthResource AuthResource { get; } = authResource;
    
    public IUserResource UserResource { get; } = userResource;
}