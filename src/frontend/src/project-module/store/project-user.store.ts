import {
  type AddManyUsersToProjectRequest,
  type AddUserToProjectRequest,
  type GetManyProjectUsersByIdsRequest,
  type GetManyProjectUsersRequest,
  type GetProjectUserByIdRequest,
  projectUserApiService,
  type RemoveManyProjectUsersRequest,
  type RemoveProjectUserRequest,
  type UpdateProjectUserRequest,
} from "src/project-module/api";
import type { ProjectUserModel } from "src/project-module/models";
import { makeAutoObservable, runInAction } from "mobx";

class ProjectUserStore {
  private _users: ProjectUserModel[] = [];

  public get users() {
    return this._users;
  }

  constructor() {
    makeAutoObservable(this);
  }

  public async fetchProjectUserById(data: GetProjectUserByIdRequest) {
    const response = await projectUserApiService.getProjectUserById(data);
    const index = this._users.findIndex((user) => user.userId === response.userId);

    if (index !== -1) {
      runInAction(() => {
        this._users[index] = response;
      });
    }
  }

  public async fetchManyProjectUsersByIds(data: GetManyProjectUsersByIdsRequest) {
    const response = await projectUserApiService.getManyProjectUsersByIds(data);

    for (const user of response.users) {
      const index = this._users.findIndex((u) => u.userId === user.userId);

      if (index !== -1) {
        runInAction(() => {
          this._users[index] = user;
        });
      }
    }
  }

  public async fetchManyProjectUsers(data: GetManyProjectUsersRequest) {
    const response = await projectUserApiService.getManyProjectUsers(data);

    runInAction(() => {
      this._users = response.projectMembers;
    });
  }

  public async addUserToProject(data: AddUserToProjectRequest) {
    const response = await projectUserApiService.addUserToProject(data);

    runInAction(() => {
      this._users.push(response);
    });
  }

  public async addManyUsersToProject(data: AddManyUsersToProjectRequest) {
    const response = await projectUserApiService.addManyUsersToProject(data);

    runInAction(() => {
      this._users = this._users.concat(response.projectMembers);
    });
  }

  public async updateProjectUser(data: UpdateProjectUserRequest) {
    const response = await projectUserApiService.updateProjectUser(data);
    const index = this._users.findIndex((user) => user.userId === response.userId);

    if (index !== -1) {
      runInAction(() => {
        this._users[index] = response;
      });
    }
  }

  public async removeProjectUser(data: RemoveProjectUserRequest) {
    await projectUserApiService.removeProjectUser(data);

    runInAction(() => {
      this._users = this._users.filter((user) => user.userId !== data.id);
    });
  }

  public async removeManyProjectUsers(data: RemoveManyProjectUsersRequest) {
    await projectUserApiService.removeManyProjectUsers(data);

    runInAction(() => {
      this._users = this._users.filter((user) => !data.ids.includes(user.userId));
    });
  }
}

export const projectUserStore = new ProjectUserStore();
