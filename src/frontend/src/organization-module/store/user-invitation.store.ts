import { makeAutoObservable, runInAction } from "mobx";
import type { UserInvitationModel } from "../models/user-invitation.ts";
import {
  type CreateUserInvitationRequest,
  type DeleteUserInvitationRequest,
  type GetManyUserInvitationsRequest,
  type ResendUserInvitationRequest,
  userInvitationApiService,
} from "src/organization-module/api";

class UserInvitationStore {
  private _invitations: UserInvitationModel[] = [];

  public get invitations(): UserInvitationModel[] {
    return this._invitations;
  }

  constructor() {
    makeAutoObservable(this);
  }

  public async fetchInvitations({
    tenantId,
  }: GetManyUserInvitationsRequest): Promise<void> {
    const res = await userInvitationApiService.getManyInvitations({ tenantId });

    runInAction(() => {
      this._invitations = res.invitations;
    });
  }

  public async createInvitation(
    data: CreateUserInvitationRequest
  ): Promise<void> {
    const res = await userInvitationApiService.createInvitation(data);

    runInAction(() => {
      this._invitations.push(res);
    });
  }

  public async resendInvitation(
    data: ResendUserInvitationRequest
  ): Promise<void> {
    await userInvitationApiService.resendInvitation(data);
  }

  public async deleteInvitation(
    data: DeleteUserInvitationRequest
  ): Promise<void> {
    await userInvitationApiService.deleteInvitation(data);

    runInAction(() => {
      this._invitations = this._invitations.filter(
        (invitation) => invitation.id !== data.invitationId
      );
    });
  }
}

export const userInvitationStore = new UserInvitationStore();
