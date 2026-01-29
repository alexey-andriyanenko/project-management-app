namespace Tenant.Contracts.Dtos;

public class TenantDto
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Slug { get; set; }
    
    public Guid OwnerUserId { get; set; }
}