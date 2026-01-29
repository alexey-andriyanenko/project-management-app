using Infrastructure.EventBus.Contracts;

namespace Tenant.Events.Contracts;

public class TenantCreatedEvent : IEvent
{
    public required Guid TenantId { get; set; }
    
    public required Guid OwnerUserId { get; set; }
}