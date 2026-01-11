using Infrastructure.EventBus.Contracts;
using Project.Contracts.Parameters.Project;
using Project.Contracts.Service;
using Tenant.Events.Contracts;

namespace Project.Events;

public class TenantEventsHandler(IProjectService projectService) : IEventHandler<TenantDeletedEvent>
{
    public async Task HandleAsync(TenantDeletedEvent @event, CancellationToken cancellationToken = default)
    {
        await projectService.DeleteManyAsync(new DeleteManyProjectsByTenantIdParameters()
        {
            TenantId = @event.TenantId
        }, cancellationToken);
    }
}