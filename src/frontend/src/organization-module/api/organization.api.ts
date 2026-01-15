import { appHttpClient } from "src/shared-module/api";
import type {
  CreateOrganizationRequest,
  CreateOrganizationResponse,
  GetManyOrganizationsResponse,
  UpdateOrganizationRequest,
} from "./organization.types.ts";
import type { OrganizationModel } from "src/organization-module/models/organization.ts";

class OrganizationApiService {
  getOrganizations() {
    return appHttpClient.get<GetManyOrganizationsResponse>("/tenants").send();
  }

  getOrganizationById(organizationId: string) {
    return appHttpClient.get<OrganizationModel>(`/tenants/${organizationId}`).send();
  }

  getOrganizationBySlug(slug: string) {
    return appHttpClient.get<OrganizationModel>(`/tenants/by-slug`)
        .setRouteParams({ slug: slug })
        .send();
  }

  createOrganization(data: CreateOrganizationRequest) {
    return appHttpClient
      .post<CreateOrganizationRequest, CreateOrganizationResponse>("/tenants")
      .send(data);
  }

  updateOrganization(data: UpdateOrganizationRequest) {
    return appHttpClient
      .put<UpdateOrganizationRequest, CreateOrganizationResponse>(`/tenants/${data.id}`)
      .send(data);
  }
}

export const organizationApiService = new OrganizationApiService();
