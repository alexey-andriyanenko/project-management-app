using Project.Contracts.Dtos;

namespace Project.Contracts.Parameters.Project;

public class CreateProjectParameters
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }

    public IReadOnlyList<CreateProjectMemberItemParameters> Members { get; set; } = [];
}

public class CreateProjectMemberItemParameters
{
    public Guid UserId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}
