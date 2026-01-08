namespace Project.Contracts.Parameters.Project;

public class UpdateProjectParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
}

