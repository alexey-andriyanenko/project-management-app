import { appHttpClient } from "src/shared-module/api";
import type {
  AddManyUsersToProjectRequest, AddManyUsersToProjectResponse,
  AddUserToProjectRequest,
  AddUserToProjectResponse,
  GetManyProjectUsersByIdsRequest,
  GetManyProjectUsersByIdsResponse,
  GetManyProjectUsersRequest,
  GetManyProjectUsersResponse,
  GetProjectUserByIdRequest,
  GetProjectUserByIdResponse,
  RemoveManyProjectUsersRequest,
  RemoveProjectUserRequest,
  UpdateProjectUserRequest,
  UpdateProjectUserResponse,
} from "src/project-module/api/project-user.types.ts";

class ProjectUserApiService {
  getProjectUserById(data: GetProjectUserByIdRequest) {
    return appHttpClient
      .get<GetProjectUserByIdResponse>(
        "/tenants/:tenantId/projects/:projectId/users/:userId",
      )
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId,
        userId: data.id,
      })
      .send();
  }

  getManyProjectUsersByIds(data: GetManyProjectUsersByIdsRequest) {
    return appHttpClient
      .post<GetManyProjectUsersByIdsRequest, GetManyProjectUsersByIdsResponse>(
        "/tenants/:tenantId/projects/:projectId/members",
      )
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId,
      })
      .setSearchParams({
        userIds: data.ids,
      })
      .send();
  }

  getManyProjectUsers(data: GetManyProjectUsersRequest) {
    return appHttpClient
      .get<GetManyProjectUsersResponse>("/tenants/:tenantId/projects/:projectId/members")
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId,
      })
      .send();
  }

  addUserToProject(data: AddUserToProjectRequest) {
    return appHttpClient
      .post<AddUserToProjectRequest, AddUserToProjectResponse>(
        "/tenants/:tenantId/projects/:projectId/members",
      )
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId,
      })
      .send(data);
  }

  addManyUsersToProject(data: AddManyUsersToProjectRequest) {
    return appHttpClient
      .post<AddManyUsersToProjectRequest, AddManyUsersToProjectResponse>(
        "/tenants/:tenantId/projects/:projectId/members/bulk",
      )
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId
      })
      .send(data);
  }

  updateProjectUser(data: UpdateProjectUserRequest) {
    return appHttpClient
      .put<UpdateProjectUserRequest, UpdateProjectUserResponse>(
        "/tenants/:tenantId/projects/:projectId/members/:userId",
      )
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId,
        userId: data.id,
      })
      .send(data);
  }

  removeProjectUser(data: RemoveProjectUserRequest): Promise<void> {
    return appHttpClient
      .delete<void>("/tenants/:tenantId/projects/:projectId/members/:userId")
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId,
        userId: data.id,
      })
      .send();
  }

  removeManyProjectUsers(data: RemoveManyProjectUsersRequest): Promise<void> {
    return appHttpClient
      .delete<void>("/tenants/:tenantId/projects/:projectId/members")
      .setRouteParams({
        tenantId: data.tenantId,
        projectId: data.projectId,
      })
      .setSearchParams({
        userIds: data.ids,
      })
      .send();
  }
}

export const projectUserApiService = new ProjectUserApiService();
