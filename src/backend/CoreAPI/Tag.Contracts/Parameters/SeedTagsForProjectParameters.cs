namespace Tag.Contracts.Parameters;

public class SeedTagsForProjectParameters
{
    public required Guid TenantId { get; set; }
    
    public required Guid ProjectId { get; set; }
}