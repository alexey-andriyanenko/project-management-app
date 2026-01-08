namespace Facade.TenantManagement.Contracts.Exception;

public class TenantMemberNotFoundException(Guid tenantId, Guid userId) : System.Exception($"Tenant member with User ID '{userId}' in Tenant ID '{tenantId}' was not found.")
{
    
}