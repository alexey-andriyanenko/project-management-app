using Infrastructure.EventBus.Contracts;

namespace Project.Events.Contracts;

public class ProjectDeletedEvent : IEvent
{
    public Guid TenantId { get; set; }
    
    public Guid ProjectId { get; set; }
}