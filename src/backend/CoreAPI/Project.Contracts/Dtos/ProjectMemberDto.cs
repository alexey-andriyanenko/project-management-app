namespace Project.Contracts.Dtos;

public class ProjectMemberDto
{
    public Guid ProjectId { get; set; }
    
    public Guid UserId { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}