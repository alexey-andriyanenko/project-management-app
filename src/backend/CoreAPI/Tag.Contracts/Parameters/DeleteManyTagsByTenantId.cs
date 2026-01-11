namespace Tag.Contracts.Parameters;

public class DeleteManyTagsByTenantId
{
    public required Guid TenantId { get; set; }
}