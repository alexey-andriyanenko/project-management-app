namespace Facade.ProjectManagement.Contracts.Dtos;

public class ProjectDto
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Description { get; set; }
    
    public required string Slug { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}