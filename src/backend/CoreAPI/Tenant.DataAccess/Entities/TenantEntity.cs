namespace Tenant.DataAccess.Entities;

public class TenantEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public ICollection<TenantMemberEntity> Members { get; private set; } = [];
}
