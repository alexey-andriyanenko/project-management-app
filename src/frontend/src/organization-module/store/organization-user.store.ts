import { makeAutoObservable, runInAction } from "mobx";
import type { OrganizationUserModel } from "../models/organization-user.ts";
import {
  type GetManyOrganizationUsersByIdsRequest,
  type GetManyOrganizationUsersRequest,
  type GetOrganizationUserByIdRequest,
  organizationUserApiService,
  type RemoveOrganizationUserRequest,
  type RetryOrganizationUserMembershipFromInvitationRequest,
} from "src/organization-module/api";

class OrganizationUsersStore {
  private _users: OrganizationUserModel[] = [];

  public get users(): OrganizationUserModel[] {
    return this._users;
  }

  constructor() {
    makeAutoObservable(this);
  }

  public async fetchUserById({
    tenantId,
    id,
  }: GetOrganizationUserByIdRequest): Promise<void> {
    const res = await organizationUserApiService.getOrganizationUserById({
      tenantId,
      id,
    });
    const index = this._users.findIndex((user) => user.userId === res.userId);

    if (index !== -1) {
      runInAction(() => {
        this._users[index] = res;
      });
    }
  }

  public async fetchManyUsersByIds({
                                     tenantId,
    ids,
  }: GetManyOrganizationUsersByIdsRequest): Promise<void> {
    const res = await organizationUserApiService.getManyOrganizationUsersByIds({
      tenantId,
      ids,
    });

    for (const user of res.tenantMembers) {
      const index = this._users.findIndex((u) => u.userId === user.userId);

      if (index !== -1) {
        runInAction(() => {
          this._users[index] = user;
        });
      }
    }
  }

  public async fetchManyUsers({ tenantId }: GetManyOrganizationUsersRequest): Promise<void> {
    const res = await organizationUserApiService.getManyOrganizationUsers({ tenantId });
    runInAction(() => {
      this._users = res.tenantMembers;
    });
  }

  public async removeUser(data: RemoveOrganizationUserRequest): Promise<void> {
    await organizationUserApiService.removeOrganizationUser(data);
    const index = this._users.findIndex((user) => user.userId === data.id);

    if (index !== -1) {
      runInAction(() => {
        this._users.splice(index, 1);
      });
    }
  }

  public async removeManyUsers(tenantId: string, ids: string[]): Promise<void> {
    await organizationUserApiService.removeManyOrganizationUsers({ tenantId, ids });

    runInAction(() => {
      this._users = this._users.filter((user) => !ids.includes(user.userId));
    });
  }

  public async retryMembershipCreationFromInvitation(data: RetryOrganizationUserMembershipFromInvitationRequest): Promise<void> {
    const res = await organizationUserApiService.retryMembershipCreationFromInvitation(data);

    runInAction(() => {
      this._users.push(res);
    });
  }
}

export const organizationUserStore = new OrganizationUsersStore();
