using Board.Contracts.Parameters.Board;
using Board.Contracts.Services;
using Infrastructure.EventBus.Contracts;
using Tenant.Events.Contracts;

namespace Board.Events;

public class TenantEventsHandler(IBoardService boardService) :  IEventHandler<TenantCreatedEvent>, IEventHandler<TenantDeletedEvent>
{
    public async Task HandleAsync(
        TenantCreatedEvent @event,
        CancellationToken cancellationToken = default)
    {
        await boardService.SeedBoardTypesForTenantAsync(@event.TenantId, cancellationToken);
    }
    
    public async Task HandleAsync(
        TenantDeletedEvent @event,
        CancellationToken cancellationToken = default)
    {
        await boardService.DeleteManyAsync(new DeleteManyBoardsByTenantId()
        {
            TenantId = @event.TenantId
        }, cancellationToken);
    }
}