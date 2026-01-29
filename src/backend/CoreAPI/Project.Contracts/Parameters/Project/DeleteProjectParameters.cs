namespace Project.Contracts.Parameters.Project;

public class DeleteProjectParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
}