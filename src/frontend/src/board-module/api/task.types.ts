import type { TaskModel } from "../models";

export type TaskResponseModel = {
  id: string;
  title: string;
  description: string;
  tenantId: string;
  projectId: string;
  boardId: string;
  boardColumn: TaskBoardColumnResponseModel;
  createdBy: TaskUserResponseModel;
  assignedTo: TaskUserResponseModel | null;
  createdAt: Date;
  updatedAt?: Date;
  tags: TaskTagResponseModel[];
};

export type TaskBoardColumnResponseModel = {
  id: string;
  name: string;
};

export type TaskUserResponseModel = {
  userId: string;
  fullName: string;
  email: string;
};

export type TaskTagResponseModel = {
  tagId: string;
  name: string;
  color: string;
};

export type GetTaskByIdRequest = {
  taskId: string;
  projectId: string;
  tenantId: string;
};

export type GetTaskByIdResponse = TaskModel;

export type GetTasksByBoardIdRequest = {
  boardId: string;
  projectId: string;
  tenantId: string;
  search?: string;
};

export type GetTasksByBoardIdResponse = {
  tasks: TaskModel[];
};

export type CreateTaskRequest = {
  tenantId: string;
  projectId: string;
  boardId: string;
  title: string;
  descriptionAsJson: string;
  descriptionAsPlainText: string;
  boardColumnId: string;
  assigneeUserId?: string;
  tagIds: string[];
};

export type CreateTaskResponse = TaskModel;

export type UpdateTaskRequest = {
  taskId: string;
  tenantId: string;
  projectId: string;
  boardId: string;
  title: string;
  descriptionAsJson: string;
  descriptionAsPlainText: string;
  boardColumnId?: string;
  assigneeUserId?: string | null;
  tagIds: string[];
};

export type UpdateTaskResponse = TaskModel;

export type DeleteTaskRequest = {
  taskId: string;
  tenantId: string;
  projectId: string;
  boardId: string;
};
