namespace Facade.TenantManagement.Contracts.Dtos;

public class TenantDto
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Slug { get; set; }
}