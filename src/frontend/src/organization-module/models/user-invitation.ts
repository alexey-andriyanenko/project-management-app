export enum UserInvitationStatus {
  Pending = 0,
  Accepted = 1,
  Expired = 2,
}

export enum MembershipCreationStatus {
  NotApplicable = 0,
  Pending = 1,
  Created = 2,
}

export interface UserInvitationModel {
  id: string;
  tenantId: string;
  invitationLink: string;
  firstName: string;
  lastName: string;
  email: string;
  tenantMemberRole: string;
  createdAt: string;
  expiresAt: string;
  acceptedAt?: string;
  status: UserInvitationStatus;
  isMembershipCreated: boolean;
  membershipStatus: MembershipCreationStatus;
}
