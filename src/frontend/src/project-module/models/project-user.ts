import type { ProjectUserRole } from "./project-user-role.ts";

export type ProjectUserModel = {
  userId: string;
  projectId: string;
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  role: ProjectUserRole;
};
