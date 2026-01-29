import type { OrganizationUserRole } from "./organization-user-role.ts";

export type OrganizationUserModel = {
  userId: string;
  tenantId: string;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  role: OrganizationUserRole;
};
