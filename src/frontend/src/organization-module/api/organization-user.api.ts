import {appHttpClient} from "src/shared-module/api";
import type {
    GetManyOrganizationUsersByIdsRequest,
    GetManyOrganizationUsersByIdsResponse,
    GetManyOrganizationUsersRequest,
    GetManyOrganizationUsersResponse,
    GetOrganizationUserByIdRequest,
    GetOrganizationUserByIdResponse,
    RetryOrganizationUserMembershipFromInvitationRequest,
    RetryOrganizationUserMembershipFromInvitationResponse,
} from "./organization-user.types.ts";

class OrganizationUserApiService {
    getOrganizationUserById(data: GetOrganizationUserByIdRequest) {
        return appHttpClient
            .get<GetOrganizationUserByIdResponse>(
                `/tenants/${data.tenantId}/members/${data.id}`,
            )
            .send();
    }

    getManyOrganizationUsersByIds(data: GetManyOrganizationUsersByIdsRequest) {
        return appHttpClient
            .get<GetManyOrganizationUsersByIdsResponse>(
                `/tenants/${data.tenantId}/members`,
            )
            .setSearchParams({
                userIds: data.ids,
            })
            .send();
    }

    getManyOrganizationUsers(data: GetManyOrganizationUsersRequest) {
        return appHttpClient
            .get<GetManyOrganizationUsersResponse>(`/tenants/${data.tenantId}/members`)
            .send();
    }

    removeOrganizationUser(data: GetOrganizationUserByIdRequest) {
        return appHttpClient
            .delete<void>(`/tenants/${data.tenantId}/members/${data.id}`)
            .send();
    }

    removeManyOrganizationUsers(data: GetManyOrganizationUsersByIdsRequest) {
        return appHttpClient
            .delete<void>(`/tenants/${data.tenantId}/members`)
            .setSearchParams({
                userIds: data.ids,
            })
            .send();
    }

    retryMembershipCreationFromInvitation(data: RetryOrganizationUserMembershipFromInvitationRequest) {
        return appHttpClient
            .post<
                { invitationId: string },
                RetryOrganizationUserMembershipFromInvitationResponse
            >(
                `/tenants/:tenantId/members/retry-from-invitation`
            )
            .setRouteParams({
                tenantId: data.tenantId,
            })
            .send({invitationId: data.invitationId});
    }
}

export const organizationUserApiService = new OrganizationUserApiService();
