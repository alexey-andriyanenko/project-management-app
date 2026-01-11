using Infrastructure.EventBus.Contracts;

namespace Project.Events.Contracts;

public class ProjectCreatedEvent : IEvent
{
    public required Guid TenantId { get; set; }
    
    public required Guid ProjectId { get; set; }
}