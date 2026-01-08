namespace Facade.ProjectManagement.Contracts.Exceptions;

public class ProjectMemberNotFoundException(Guid projectId, Guid userId) : Exception($"Project member with UserId '{userId}' in Project '{projectId}' was not found.")
{
    
}