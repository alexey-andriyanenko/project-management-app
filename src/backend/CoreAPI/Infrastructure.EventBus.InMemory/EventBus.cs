using Infrastructure.EventBus.Contracts;

namespace Infrastructure.EventBus.InMemory;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public sealed class InMemoryEventBus(
    IServiceProvider serviceProvider,
    ILogger<InMemoryEventBus> logger)
    : IEventBus
{
    public async Task PublishAsync<TEvent>(
        TEvent @event,
        CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        using var scope = serviceProvider.CreateScope();

        var handlers = scope.ServiceProvider
            .GetServices<IEventHandler<TEvent>>()
            .ToList();

        if (handlers.Count == 0)
            return;

        var tasks = handlers.Select(h =>
            SafeHandleAsync(h, @event, cancellationToken));

        await Task.WhenAll(tasks);
    }

    private async Task SafeHandleAsync<TEvent>(
        IEventHandler<TEvent> handler,
        TEvent @event,
        CancellationToken ct)
        where TEvent : IEvent
    {
        try
        {
            await handler.HandleAsync(@event, ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Error handling event {EventType} with handler {Handler}",
                typeof(TEvent).Name,
                handler.GetType().Name);
        }
    }
}
