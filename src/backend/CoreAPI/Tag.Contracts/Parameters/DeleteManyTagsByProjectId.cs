namespace Tag.Contracts.Parameters;

public class DeleteManyTagsByProjectId
{
    public required Guid TenantId { get; set; }
    
    public required Guid ProjectId { get; set; }
}