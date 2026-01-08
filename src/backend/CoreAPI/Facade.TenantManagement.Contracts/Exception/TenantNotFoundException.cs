namespace Facade.TenantManagement.Contracts.Exception;

public class TenantNotFoundException(Guid tenantId) : System.Exception($"Tenant with ID '{tenantId}' was not found.")
{
    
}