import { type ProjectUserModel, ProjectUserRole } from "src/project-module/models";

export type GetProjectUserByIdRequest = {
  tenantId: string;
  projectId: string;
  id: string;
};

export type GetProjectUserByIdResponse = ProjectUserModel;

export type GetManyProjectUsersByIdsRequest = {
  tenantId: string;
  projectId: string;
  ids: string[];
};

export type GetManyProjectUsersByIdsResponse = {
  users: ProjectUserModel[];
};

export type GetManyProjectUsersRequest = {
  tenantId: string;
  projectId: string;
};

export type GetManyProjectUsersResponse = {
  projectMembers: ProjectUserModel[];
};

export type AddUserToProjectRequest = {
  tenantId: string;
  projectId: string;
  userId: string;
  role: ProjectUserRole;
};

export type AddUserToProjectResponse = ProjectUserModel;

export type AddManyUsersToProjectRequest = {
  tenantId: string;
  projectId: string;
  members: AddUserToProjectItem[];
};

export type AddUserToProjectItem = {
  userId: string;
  role: ProjectUserRole;
};

export type AddManyUsersToProjectResponse = {
  projectMembers: ProjectUserModel[];
};

export type UpdateProjectUserRequest = {
  tenantId: string;
  projectId: string;
  id: string;
  role: ProjectUserRole;
};

export type UpdateProjectUserResponse = ProjectUserModel;

export type RemoveProjectUserRequest = {
  tenantId: string;
  projectId: string;
  id: string;
};

export type RemoveManyProjectUsersRequest = {
  tenantId: string;
  projectId: string;
  ids: string[];
};
