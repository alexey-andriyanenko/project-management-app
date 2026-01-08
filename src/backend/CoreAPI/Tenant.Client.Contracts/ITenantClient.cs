using Tenant.Client.Contracts.Resources;

namespace Tenant.Client.Contracts;

public interface ITenantClient
{
    public ITenantResource TenantResource { get; }
    
    public ITenantMemberResource TenantMemberResource { get; }
}