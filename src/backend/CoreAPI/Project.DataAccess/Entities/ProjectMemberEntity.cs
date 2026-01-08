using Project.DataAccess.Enums;

namespace Project.DataAccess.Entities;

public class ProjectMemberEntity
{
    public Guid UserId { get; set; }
    
    public Guid ProjectId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}