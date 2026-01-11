using Infrastructure.EventBus.Contracts;
using Tag.Contracts.Services;
using Tenant.Events.Contracts;

namespace Tag.Events;

public class TenantEventsHandler(ITagService tagService) : IEventHandler<TenantCreatedEvent>, IEventHandler<TenantDeletedEvent>
{
    public async Task HandleAsync(TenantCreatedEvent @event, CancellationToken cancellationToken)
    {
        await tagService.SeedTagsForTenantAsync(new Tag.Contracts.Parameters.SeedTagsForTenantParameters
        {
            TenantId = @event.TenantId
        }, cancellationToken);
    }
    
    public async Task HandleAsync(TenantDeletedEvent @event, CancellationToken cancellationToken)
    {
        await tagService.DeleteManyAsync(new Tag.Contracts.Parameters.DeleteManyTagsByTenantId
        {
            TenantId = @event.TenantId
        }, cancellationToken);
    }
}