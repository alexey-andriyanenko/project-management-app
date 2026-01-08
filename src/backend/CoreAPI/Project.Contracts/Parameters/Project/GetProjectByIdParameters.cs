namespace Project.Contracts.Parameters.Project;

public class GetProjectByIdParameters
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
}