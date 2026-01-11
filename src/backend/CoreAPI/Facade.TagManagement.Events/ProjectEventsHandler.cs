using Facade.ProjectManagement.Events.Contracts;
using Infrastructure.EventBus.Contracts;

namespace Facade.TagManagement.Events;

public class ProjectEventsHandler : IEventHandler<ProjectCreatedEvent>, IEventHandler<ProjectDeletedEvent>
{
    
}