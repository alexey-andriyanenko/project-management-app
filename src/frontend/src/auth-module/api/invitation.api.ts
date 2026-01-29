import { anonymousHttpClient } from "src/shared-module/api";

export type ValidateInvitationRequest = {
  invitationToken: string;
};

export type ValidateInvitationResponse = {
  isValid: boolean;
  firstName?: string;
  lastName?: string;
  email?: string;
  tenantMemberRole?: string;
};

export type AcceptInvitationRequest = {
  invitationToken: string;
  userName: string;
  password: string;
};

class InvitationApiService {
  validateInvitation(data: ValidateInvitationRequest) {
    return anonymousHttpClient
      .post<ValidateInvitationRequest, ValidateInvitationResponse>(
        "/user-invitations/validate"
      )
      .send(data);
  }

  acceptInvitation(data: AcceptInvitationRequest) {
    return anonymousHttpClient
      .post<AcceptInvitationRequest, void>("/user-invitations/accept")
      .send(data);
  }
}

export const invitationApiService = new InvitationApiService();
