import { makeAutoObservable, runInAction } from "mobx";
import type { OrganizationModel } from "../models/organization.ts";
import { organizationApiService } from "../api/organization.api.ts";
import type {
  CreateOrganizationRequest,
  UpdateOrganizationRequest,
  DeleteOrganizationRequest,
} from "src/organization-module/api";

class OrganizationStore {
  private _currentOrganization: OrganizationModel | null = null;

  private _organizations: OrganizationModel[] = [];

  public get currentOrganization(): OrganizationModel | null {
    return this._currentOrganization;
  }

  public get organizations(): OrganizationModel[] {
    return this._organizations;
  }

  constructor() {
    makeAutoObservable(this);
  }

  public setCurrentOrganization(organization: OrganizationModel): void {
    runInAction(() => {
      this._currentOrganization = organization;
    });
  }

  public async fetchOrganizations(): Promise<void> {
    const res = await organizationApiService.getOrganizations();

    runInAction(() => {
      this._organizations = res.tenants;
    });
  }

  public async fetchCurrentOrganizationBySlug(slug: string): Promise<void> {
    const res = await organizationApiService.getOrganizationBySlug(slug);

    runInAction(() => {
      this._currentOrganization = res;
    });
  }

  public async createOrganization(data: CreateOrganizationRequest): Promise<void> {
    const res = await organizationApiService.createOrganization(data);

    runInAction(() => {
      this._organizations.push(res);
    });
  }

  public async updateOrganization(data: UpdateOrganizationRequest): Promise<void> {
    const res = await organizationApiService.updateOrganization({
      id: data.id,
      name: data.name,
    });

    runInAction(() => {
      if (this._currentOrganization?.id === res.id) {
        this._currentOrganization = res;
      }

      const index = this._organizations.findIndex((org) => org.id === res.id);
      if (index !== -1) {
        this._organizations[index] = res;
      }
    });
  }

  public async deleteOrganization(data: DeleteOrganizationRequest): Promise<void> {
    await organizationApiService.deleteOrganization(data);

    runInAction(() => {
      if (this._currentOrganization?.id === data.id) {
        this._currentOrganization = null;
      }

      this._organizations = this._organizations.filter((org) => org.id !== data.id);
    });
  }
}

export const organizationStore = new OrganizationStore();
