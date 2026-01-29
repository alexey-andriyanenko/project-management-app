import { appHttpClient } from "src/shared-module/api";
import type {
  CreateUserInvitationRequest,
  CreateUserInvitationResponse,
  DeleteUserInvitationRequest,
  GetManyUserInvitationsRequest,
  GetManyUserInvitationsResponse,
  ResendUserInvitationRequest,
} from "./user-invitation.types.ts";

class UserInvitationApiService {
  getManyInvitations(data: GetManyUserInvitationsRequest) {
    return appHttpClient
      .get<GetManyUserInvitationsResponse>("/user-invitations")
      .setSearchParams({ tenantId: data.tenantId })
      .send();
  }

  createInvitation(data: CreateUserInvitationRequest) {
    return appHttpClient
      .post<CreateUserInvitationRequest, CreateUserInvitationResponse>(
        "/user-invitations"
      )
      .send(data);
  }

  resendInvitation(data: ResendUserInvitationRequest) {
    return appHttpClient
      .post<void, void>(`/user-invitations/${data.invitationId}/resend`)
      .send();
  }

  deleteInvitation(data: DeleteUserInvitationRequest) {
    return appHttpClient
      .delete<void>(`/user-invitations/${data.invitationId}`)
      .send();
  }
}

export const userInvitationApiService = new UserInvitationApiService();
