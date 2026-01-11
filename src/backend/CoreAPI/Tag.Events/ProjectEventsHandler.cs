using Infrastructure.EventBus.Contracts;
using Project.Events.Contracts;
using Tag.Contracts.Services;

namespace Tag.Events;

public class ProjectEventsHandler(ITagService tagService) : IEventHandler<ProjectCreatedEvent>, IEventHandler<ProjectDeletedEvent>
{
    public async Task HandleAsync(ProjectCreatedEvent @event, CancellationToken cancellationToken)
    {
        await tagService.SeedTagsForProjectAsync(new Tag.Contracts.Parameters.SeedTagsForProjectParameters
        {
            TenantId = @event.TenantId,
            ProjectId = @event.ProjectId
        }, cancellationToken);
    }
    
    public async Task HandleAsync(ProjectDeletedEvent @event, CancellationToken cancellationToken)
    {
        await tagService.DeleteManyAsync(new Tag.Contracts.Parameters.DeleteManyTagsByProjectId
        {
            TenantId = @event.TenantId,
            ProjectId = @event.ProjectId
        }, cancellationToken);
    }
}