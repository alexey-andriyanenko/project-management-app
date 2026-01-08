namespace Tag.Contracts.Parameters;

public class GetManyTagsByIdsParameters
{
    public Guid TenantId { get; set; }
    
    public IReadOnlyList<Guid> TagIds { get; set; } = [];
}