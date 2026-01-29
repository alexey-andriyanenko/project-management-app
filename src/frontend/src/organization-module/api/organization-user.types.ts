import type { OrganizationUserModel } from "src/organization-module/models/organization-user.ts";
import type { OrganizationUserRole } from "src/organization-module/models/organization-user-role.ts";

export type GetOrganizationUserByIdRequest = {
  tenantId: string;
  id: string;
};

export type GetOrganizationUserByIdResponse = OrganizationUserModel;

export type GetManyOrganizationUsersByIdsRequest = {
  tenantId: string;
  ids: string[];
};

export type GetManyOrganizationUsersByIdsResponse = {
  tenantMembers: OrganizationUserModel[];
};

export type GetManyOrganizationUsersRequest = {
  tenantId: string;
};

export type GetManyOrganizationUsersResponse = {
  tenantMembers: OrganizationUserModel[];
};

export type CreateOrganizationUserRequest = {
  tenantId: string;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  password: string;
  role: OrganizationUserRole;
};

export type CreateOrganizationUserResponse = OrganizationUserModel;

export type UpdateOrganizationUserRequest = CreateOrganizationUserRequest & {
  id: string;
};

export type UpdateOrganizationUserResponse = OrganizationUserModel;

export type RemoveOrganizationUserRequest = {
  tenantId: string;
  id: string;
};

export type RemoveManyOrganizationUsersRequest = {
  tenantId: string;
  ids: string[];
};

export type RetryOrganizationUserMembershipFromInvitationRequest = {
  tenantId: string;
  invitationId: string;
};

export type RetryOrganizationUserMembershipFromInvitationResponse = OrganizationUserModel;
