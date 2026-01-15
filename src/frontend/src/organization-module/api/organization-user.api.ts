import { appHttpClient } from "src/shared-module/api";
import type {
  CreateOrganizationUserRequest,
  CreateOrganizationUserResponse,
  GetManyOrganizationUsersByIdsRequest,
  GetManyOrganizationUsersByIdsResponse,
  GetManyOrganizationUsersRequest,
  GetManyOrganizationUsersResponse,
  GetOrganizationUserByIdRequest,
  GetOrganizationUserByIdResponse,
  UpdateOrganizationUserRequest,
  UpdateOrganizationUserResponse,
} from "./organization-user.types.ts";

class OrganizationUserApiService {
  getOrganizationUserById(data: GetOrganizationUserByIdRequest) {
    return appHttpClient
      .get<GetOrganizationUserByIdResponse>(
        `/tenants/${data.organizationId}/members/${data.id}`,
      )
      .send();
  }

  getManyOrganizationUsersByIds(data: GetManyOrganizationUsersByIdsRequest) {
    return appHttpClient
      .get<GetManyOrganizationUsersByIdsResponse>(
        `/tenants/${data.organizationId}/members`,
      )
      .setSearchParams({
        userIds: data.ids,
      })
      .send();
  }

  getManyOrganizationUsers(data: GetManyOrganizationUsersRequest) {
    return appHttpClient
      .get<GetManyOrganizationUsersResponse>(`/tenants/${data.organizationId}/members`)
      .send();
  }

  createOrganizationUser(data: CreateOrganizationUserRequest) {
    return appHttpClient
      .post<
        CreateOrganizationUserRequest,
        CreateOrganizationUserResponse
      >(`/tenants/${data.organizationId}/members`)
      .send(data);
  }

  updateOrganizationUser(data: UpdateOrganizationUserRequest) {
    return appHttpClient
      .put<
        UpdateOrganizationUserRequest,
        UpdateOrganizationUserResponse
      >(`/tenants/${data.organizationId}/members/${data.id}`)
      .send(data);
  }

  removeOrganizationUser(data: GetOrganizationUserByIdRequest) {
    return appHttpClient
      .delete<void>(`/tenants/${data.organizationId}/members/${data.id}`)
      .send();
  }

  removeManyOrganizationUsers(data: GetManyOrganizationUsersByIdsRequest) {
    return appHttpClient
      .delete<void>(`/tenants/${data.organizationId}/members`)
      .setSearchParams({
        userIds: data.ids,
      })
      .send();
  }
}

export const organizationUserApiService = new OrganizationUserApiService();
