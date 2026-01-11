using Board.Contracts.Services;
using Infrastructure.EventBus.Contracts;
using Project.Events.Contracts;

namespace Board.Events;

public class ProjectEventsHandler(IBoardService boardService) : IEventHandler<ProjectDeletedEvent>
{
    public async Task HandleAsync(
        ProjectDeletedEvent @event,
        CancellationToken cancellationToken = default)
    {
        await boardService.DeleteManyAsync(new Contracts.Parameters.Board.DeleteManyBoardsByProjectId()
        {
            TenantId = @event.TenantId,
            ProjectId = @event.ProjectId
        }, cancellationToken);
    }
}