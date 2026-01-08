using Tenant.Client.Contracts;
using Tenant.Client.Contracts.Resources;

namespace Tenant.Client;

public class TenantClient(ITenantResource tenantResource, ITenantMemberResource tenantMemberResource) : ITenantClient
{
    public ITenantResource TenantResource { get; } = tenantResource;
    
    public ITenantMemberResource TenantMemberResource { get; } = tenantMemberResource;
}