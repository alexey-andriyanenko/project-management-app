using Infrastructure.EventBus.Contracts;

namespace Tenant.Events.Contracts;

public class TenantDeletedEvent : IEvent
{
    public required Guid TenantId { get; set; }
}