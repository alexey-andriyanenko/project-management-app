import type { UserInvitationModel } from "../models/user-invitation.ts";

export type GetManyUserInvitationsRequest = {
  tenantId: string;
};

export type GetManyUserInvitationsResponse = {
  invitations: UserInvitationModel[];
};

export type CreateUserInvitationRequest = {
  tenantId: string;
  email: string;
  firstName: string;
  lastName: string;
  tenantMemberRole: string;
};

export type CreateUserInvitationResponse = UserInvitationModel;

export type ResendUserInvitationRequest = {
  invitationId: string;
};

export type DeleteUserInvitationRequest = {
  invitationId: string;
};
