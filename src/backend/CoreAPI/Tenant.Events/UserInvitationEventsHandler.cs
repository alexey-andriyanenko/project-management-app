using Identity.Events.Contracts;
using Infrastructure.EventBus.Contracts;
using Tenant.Contracts.Parameters;
using Tenant.Contracts.Services;

namespace Tenant.Events;

public class UserInvitationEventsHandler(ITenantMemberService tenantMemberService) : IEventHandler<UserInvitationAcceptedEvent>
{
    public async Task HandleAsync(UserInvitationAcceptedEvent @event, CancellationToken cancellationToken = default)
    {
        await tenantMemberService.CreateAsync(new CreateTenantMemberParameters()
        {
            UserId = @event.UserId,
            TenantId = @event.TenantId,
            Role = Enum.Parse<Tenant.Contracts.Dtos.TenantMemberRole>(@event.TenantMemberRole)
        });
    }
}