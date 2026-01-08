using Facade.ProjectManagement.Contracts.Dtos;

namespace Facade.ProjectManagement.Contracts.Parameters.Project;

public class UpdateProjectParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public IReadOnlyList<UpdateProjectMemberItemParameters> Members { get; set; } = Array.Empty<UpdateProjectMemberItemParameters>();
}

public class UpdateProjectMemberItemParameters
{
    public Guid UserId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}
