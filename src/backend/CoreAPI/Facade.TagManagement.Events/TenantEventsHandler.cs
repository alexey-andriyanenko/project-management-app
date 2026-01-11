using Facade.ProjectManagement.Events.Contracts;
using Facade.TenantManagement.Events.Contracts.Events;
using Infrastructure.EventBus.Contracts;

namespace Facade.TagManagement.Events;

public class TenantEventsHandler : IEventHandler<TenantCreatedEvent>, IEventHandler<TenantDeletedEvent>
{
    public Task HandleAsync(TenantCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public Task HandleAsync(TenantDeletedEvent @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}