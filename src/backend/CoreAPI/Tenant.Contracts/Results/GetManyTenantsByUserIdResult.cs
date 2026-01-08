using Tenant.Contracts.Dtos;

namespace Tenant.Contracts.Results;

public class GetManyTenantsByUserIdResult
{
    public IReadOnlyList<TenantDto> Tenants { get; set; } = [];
}