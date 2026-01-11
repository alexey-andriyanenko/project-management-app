using Infrastructure.EventBus.Contracts;

namespace Tenant.Events.Contracts;

public class TenantCreatedEvent : IEvent
{
    public required Guid TenantId { get; set; }
}