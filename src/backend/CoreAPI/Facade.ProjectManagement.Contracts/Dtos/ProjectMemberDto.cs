namespace Facade.ProjectManagement.Contracts.Dtos;

public class ProjectMemberDto
{
    public Guid ProjectId { get; set; }
    
    public Guid UserId { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required string Email { get; set; }
    
    public ProjectMemberRole Role { get; set; }
}