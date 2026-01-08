using Facade.TenantManagement.Contracts.Dtos;

namespace Facade.TenantManagement.Contracts.Results;

public class GetManyTenantsByUserIdResult
{
    public IReadOnlyList<TenantDto> Tenants { get; set; } = [];
}