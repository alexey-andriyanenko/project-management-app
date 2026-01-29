using Infrastructure.EventBus.Contracts;

namespace Identity.Events.Contracts;

public class UserInvitationAcceptedEvent : IEvent
{
    public Guid UserId { get; set; }
    
    public Guid TenantId { get; set; }
    
    public required string TenantMemberRole { get; set; }
}
