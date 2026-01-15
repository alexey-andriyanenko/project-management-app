namespace Project.Contracts.Parameters.Project;

public class GetProjectBySlugParameters
{
    public required Guid TenantId { get; set; }
    
    public required Guid MemberId { get; set; }
    
    public required string Slug { get; set; }
}