import type {
    CreateTaskRequest,
    CreateTaskResponse,
    DeleteTaskRequest,
    GetTaskByIdRequest,
    GetTaskByIdResponse,
    GetTasksByBoardIdRequest,
    GetTasksByBoardIdResponse,
    TaskTagResponseModel,
    TaskBoardColumnResponseModel,
    TaskResponseModel,
    TaskUserResponseModel,
    UpdateTaskRequest,
    UpdateTaskResponse,
} from "./task.types.ts";
import {appHttpClient} from "src/shared-module/api";
import type {
    TaskBoardColumnModel,
    TaskModel,
    TaskTagModel,
    TaskUserModel,
} from "src/board-module/models";

class TaskApiService {
    public async getTaskById(data: GetTaskByIdRequest): Promise<GetTaskByIdResponse> {
        const response = await appHttpClient
            .get<TaskResponseModel>(
                `/tenants/:tenantId/projects/:projectId/tasks/:taskId`,
            )
            .setRouteParams({
                tenantId: data.tenantId,
                projectId: data.projectId,
                taskId: data.taskId,
            })
            .send();

        return this.toTaskDomainModel(response);
    }

    public async getTasksByBoardId(
        data: GetTasksByBoardIdRequest,
    ): Promise<GetTasksByBoardIdResponse> {
        const response = await appHttpClient
            .get<{
                tasks: TaskResponseModel[];
            }>(`/tenants/:tenantId/projects/:projectId/tasks`)
            .setSearchParams({boardId: data.boardId, search: data.search ?? ""})
            .setRouteParams({
                tenantId: data.tenantId,
                projectId: data.projectId,
            })
            .send();

        return {
            tasks: response.tasks.map((task) => this.toTaskDomainModel(task)),
        };
    }

    public async createTask(data: CreateTaskRequest): Promise<TaskModel> {
        const response = await appHttpClient
            .post<
                CreateTaskRequest,
                CreateTaskResponse
            >(`/tenants/:tenantId/projects/:projectId/tasks`)
            .setRouteParams({
                tenantId: data.tenantId,
                projectId: data.projectId,
            })
            .send(data);

        return this.toTaskDomainModel(response);
    }

    public async updateTask(data: UpdateTaskRequest): Promise<TaskModel> {
        const response = await appHttpClient
            .put<
                UpdateTaskRequest,
                UpdateTaskResponse
            >(`/tenants/:tenantId/projects/:projectId/tasks/:taskId`)
            .setRouteParams({
                tenantId: data.tenantId,
                projectId: data.projectId,
                taskId: data.taskId,
            })
            .setSearchParams({
                boardId: data.boardId,
            })
            .send(data);

        return this.toTaskDomainModel(response);
    }

    public async deleteTask(data: DeleteTaskRequest): Promise<void> {
        await appHttpClient
            .delete(
                `/tenants/:tenantId/projects/:projectId/tasks/:taskId`,
            )
            .setRouteParams({
                tenantId: data.tenantId,
                projectId: data.projectId,
                taskId: data.taskId,
            })
            .setSearchParams({
                boardId: data.boardId,
            })
            .send();
    }

    // Mapping functions ↓
    private toTaskDomainModel(response: TaskResponseModel): TaskModel {
        return {
            id: response.id,
            title: response.title,
            description: response.description,
            tenantId: response.tenantId,
            projectId: response.projectId,
            boardId: response.boardId,
            boardColumn: this.toTaskBoardColumnDomainModel(response.boardColumn),
            createdBy: this.toTaskUserDomainModel(response.createdBy),
            assignedTo: response.assignedTo ? this.toTaskUserDomainModel(response.assignedTo) : null,
            createdAt: new Date(response.createdAt),
            updatedAt: response.updatedAt ? new Date(response.updatedAt) : undefined,
            tags: response.tags.map(this.toTaskTagDomainModel),
        };
    }

    private toTaskUserDomainModel(response: TaskUserResponseModel): TaskUserModel {
        return {
            userId: response.userId,
            fullName: response.fullName,
            email: response.email,
        };
    }

    private toTaskBoardColumnDomainModel(
        response: TaskBoardColumnResponseModel,
    ): TaskBoardColumnModel {
        return {
            id: response.id,
            name: response.name,
        };
    }

    private toTaskTagDomainModel(response: TaskTagResponseModel): TaskTagModel {
        return {
            tagId: response.tagId,
            name: response.name,
            color: response.color,
        };
    }
}

export const taskApiService = new TaskApiService();
