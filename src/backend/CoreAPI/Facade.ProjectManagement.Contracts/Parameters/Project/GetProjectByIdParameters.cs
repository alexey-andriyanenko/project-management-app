namespace Facade.ProjectManagement.Contracts.Parameters.Project;

public class GetProjectByIdParameters
{
    public Guid UserId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public Guid TenantId { get; set; }
}