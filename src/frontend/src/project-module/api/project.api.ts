import { appHttpClient } from "src/shared-module/api";
import type {
  CreateProjectRequest,
  CreateProjectResponse,
  GetProjectsResponse,
  UpdateProjectRequest,
  UpdateProjectResponse,
} from "./project.types.ts";

class ProjectApiService {
  public async getProjectById(tenantId: string, projectId: string) {
    return await appHttpClient
      .get<CreateProjectResponse>("/tenants/:tenantId/projects/:projectId")
      .setRouteParams({ tenantId, projectId })
      .send();
  }

  public async getProjectBySlug(tenantId: string, projectSlug: string) {
    return await appHttpClient
      .get<CreateProjectResponse>("/tenants/:tenantId/projects/by-slug")
      .setRouteParams({ tenantId })
      .setSearchParams({ slug: projectSlug })
      .send();
  }

  public async getManyProjects(tenantId: string) {
    return await appHttpClient
      .get<GetProjectsResponse>("/tenants/:tenantId/projects")
      .setRouteParams({ tenantId })
      .send();
  }

  public async createProject(data: CreateProjectRequest) {
    return await appHttpClient
      .post<CreateProjectRequest, CreateProjectResponse>("/tenants/:tenantId/projects")
      .setRouteParams({ tenantId: data.tenantId })
      .send(data);
  }

  public async updateProject(data: UpdateProjectRequest) {
    return await appHttpClient
      .put<
        UpdateProjectRequest,
        UpdateProjectResponse
      >("/tenants/:tenantId/projects/:projectId")
      .setRouteParams({ tenantId: data.tenantId, projectId: data.projectId })
      .send(data);
  }

  public async deleteProject(tenantId: string, projectId: string) {
    return await appHttpClient
      .delete<void>("/tenants/:tenantId/projects/:projectId")
      .setRouteParams({ tenantId, projectId })
      .send();
  }
}

export const projectApiService = new ProjectApiService();
