namespace Project.Contracts.Exceptions;

public class ProjectNotFoundException : Exception
{
    public ProjectNotFoundException(Guid projectId) : base($"Project with ID '{projectId}' was not found.")
    {
    }
    
    public ProjectNotFoundException(string slug) : base($"Project with Slug '{slug}' was not found.")
    {}
}