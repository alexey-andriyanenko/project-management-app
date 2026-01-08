using Project.Client.Contracts.Resources;

namespace Project.Client.Contracts;

public interface IProjectClient
{
    public IProjectResource ProjectResource { get; }
    
    public IProjectMemberResource ProjectMemberResource { get; }
}