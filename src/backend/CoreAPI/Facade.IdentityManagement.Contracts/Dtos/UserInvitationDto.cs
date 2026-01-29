namespace Facade.IdentityManagement.Contracts.Dtos;

public class UserInvitationDto
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public required string InvitationLink { get; set; }
    
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string TenantMemberRole { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public bool IsMembershipCreated { get; set; }

    public UserInvitationStatus Status
    {
        get
        {
            if (AcceptedAt.HasValue)
            {
                return UserInvitationStatus.Accepted;
            }

            if (DateTime.UtcNow > ExpiresAt)
            {
                return UserInvitationStatus.Expired;
            }

            return UserInvitationStatus.Pending;
        }
    }

    public MembershipCreationStatus MembershipStatus
    {
        get
        {
            if (!AcceptedAt.HasValue)
            {
                return MembershipCreationStatus.NotApplicable;
            }

            if (IsMembershipCreated)
            {
                return MembershipCreationStatus.Created;
            }

            return MembershipCreationStatus.Pending;
        }
    }
}

public enum UserInvitationStatus
{
    Pending,
    Accepted,
    Expired
}

public enum MembershipCreationStatus
{
    NotApplicable,
    Pending,
    Created
}
