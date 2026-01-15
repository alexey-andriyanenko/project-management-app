namespace Facade.TenantManagement.Contracts.Parameters;

public class GetTenantBySlugParameters
{
    public required Guid MemberId { get; set; }
    
    public required string Slug { get; set; }
}