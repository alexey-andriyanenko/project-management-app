namespace Facade.ProjectManagement.Contracts.Exceptions;

public class ProjectNotFoundException(Guid projectId) : Exception($"Project with ID '{projectId}' was not found.")
{
    
}