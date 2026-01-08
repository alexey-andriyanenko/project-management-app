namespace Tenant.Contracts.Exceptions;

public class TenantMemberNotFoundException(Guid tenantId, Guid userId) : Exception($"Tenant member with User ID '{userId}' in Tenant ID '{tenantId}' was not found.")
{
    
}