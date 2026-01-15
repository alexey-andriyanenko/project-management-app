import { appHttpClient } from "src/shared-module/api";
import type {
  CreateProjectRequest,
  CreateProjectResponse,
  GetProjectsResponse,
  UpdateProjectRequest,
  UpdateProjectResponse,
} from "./project.types.ts";

class ProjectApiService {
  public async getProjectById(organizationId: string, projectId: string) {
    return await appHttpClient
      .get<CreateProjectResponse>("/tenants/:organizationId/projects/:projectId")
      .setRouteParams({ organizationId, projectId })
      .send();
  }

  public async getProjectBySlug(organizationId: string, projectSlug: string) {
    return await appHttpClient
      .get<CreateProjectResponse>("/tenants/:organizationId/projects/by-slug")
      .setRouteParams({ organizationId })
      .setSearchParams({ slug: projectSlug })
      .send();
  }

  public async getManyProjects(organizationId: string) {
    return await appHttpClient
      .get<GetProjectsResponse>("/tenants/:organizationId/projects")
      .setRouteParams({ organizationId })
      .send();
  }

  public async createProject(data: CreateProjectRequest) {
    return await appHttpClient
      .post<CreateProjectRequest, CreateProjectResponse>("/tenants/:organizationId/projects")
      .setRouteParams({ organizationId: data.organizationId })
      .send(data);
  }

  public async updateProject(data: UpdateProjectRequest) {
    return await appHttpClient
      .put<
        UpdateProjectRequest,
        UpdateProjectResponse
      >("/tenants/:organizationId/projects/:projectId")
      .setRouteParams({ organizationId: data.organizationId, projectId: data.projectId })
      .send(data);
  }

  public async deleteProject(organizationId: string, projectId: string) {
    return await appHttpClient
      .delete<void>("/tenants/:organizationId/projects/:projectId")
      .setRouteParams({ organizationId, projectId })
      .send();
  }
}

export const projectApiService = new ProjectApiService();
