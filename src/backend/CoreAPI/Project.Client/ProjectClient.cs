using Project.Client.Contracts;
using Project.Client.Contracts.Resources;

namespace Project.Client;

public class ProjectClient(IProjectResource projectResource, IProjectMemberResource projectMemberResource) : IProjectClient
{
    public IProjectResource ProjectResource { get; } = projectResource;
    
    public IProjectMemberResource ProjectMemberResource { get; } = projectMemberResource;
}