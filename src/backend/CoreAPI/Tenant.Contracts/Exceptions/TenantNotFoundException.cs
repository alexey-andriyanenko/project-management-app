namespace Tenant.Contracts.Exceptions;

public class TenantNotFoundException(Guid tenantId) : Exception($"Tenant with ID '{tenantId}' was not found.")
{
    
}