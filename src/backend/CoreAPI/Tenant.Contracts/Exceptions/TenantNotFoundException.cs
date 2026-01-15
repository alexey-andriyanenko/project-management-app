namespace Tenant.Contracts.Exceptions;

public class TenantNotFoundException : Exception
{
    public TenantNotFoundException(string slug) : base($"Tenant with slug '{slug}' was not found.")
    {
    }
    
    public TenantNotFoundException(Guid tenantId) : base($"Tenant with ID '{tenantId}' was not found.")
    {
    }
}