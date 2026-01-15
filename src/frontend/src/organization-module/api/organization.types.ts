import type { OrganizationModel } from "../models/organization.ts";

export type GetManyOrganizationsResponse = {
  tenants: OrganizationModel[];
};

export type CreateOrganizationRequest = {
  name: string;
};

export type CreateOrganizationResponse = OrganizationModel;

export type UpdateOrganizationRequest = CreateOrganizationRequest & {
  id: string;
};

export type UpdateOrganizationResponse = OrganizationModel;
