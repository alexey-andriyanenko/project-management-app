namespace Facade.TagManagement.Contracts.Parameters;

public class GetManyTagsByTenantIdParameters
{
    public Guid TenantId { get; set; }
    
    public Guid? ProjectId { get; set; }
}