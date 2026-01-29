import type { ProjectModel } from "../models/project.ts";
import type { ProjectUserRole } from "src/project-module/models/project-user-role.ts";

export type GetProjectsResponse = {
  projects: ProjectModel[];
};

export type CreateProjectRequest = {
  name: string;
  description: string;
  tenantId: string;
  members: CreateProjectUserItem[];
};

export type CreateProjectUserItem = {
  userId: string;
  role: ProjectUserRole;
};

export type CreateProjectResponse = ProjectModel;

export type UpdateProjectRequest = {
  projectId: string;
  tenantId: string;
  name: string;
  description: string;
};

export type UpdateProjectResponse = ProjectModel;
