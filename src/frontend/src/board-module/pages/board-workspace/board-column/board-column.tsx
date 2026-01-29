import React from "react";
import { observer } from "mobx-react-lite";
import { Flex, Heading, IconButton, Stack } from "@chakra-ui/react";
import { LuPlus } from "react-icons/lu";
import { useDroppable } from "@dnd-kit/core";

import {
  useBoardStore,
  useModalsStore as useBoardModalsStore,
  useTaskStore,
} from "src/board-module/store";
import type { BoardColumnModel, TaskModel } from "src/board-module/models";
import { useOrganizationStore } from "src/organization-module/store";
import { useModalsStore as useSharedModalsStore } from "src/shared-module/store/modals";
import { taskStore } from "src/board-module/store/task.store.ts";
import { TaskCard } from "src/board-module/pages/board-workspace/board-column/task-card";
import { extractTextFromTiptap } from "src/board-module/utils";

type BoardColumnProps = {
  column: BoardColumnModel;
};

export const BoardColumn: React.FC<BoardColumnProps> = observer(({ column }) => {
  const organizationStore = useOrganizationStore();
  const boardStore = useBoardStore();
  const boardModalsStore = useBoardModalsStore();
  const tasksStore = useTaskStore();
  const sharedModalsStore = useSharedModalsStore();
  
  const { setNodeRef, isOver } = useDroppable({
    id: column.id,
  });

  const handleCreateTask = () => {
    boardModalsStore.open("CreateOrEditTaskDialog", {
      board: boardStore.currentBoard!,
      boardColumn: column,
      onCreate: (data) =>
        tasksStore.createTask({
          tenantId: organizationStore.currentOrganization!.id,
          projectId: boardStore.currentBoard!.projectId,
          boardId: boardStore.currentBoard!.id,
          title: data.title,
          descriptionAsJson: JSON.stringify(data.description),
          descriptionAsPlainText: extractTextFromTiptap(data.description),
          boardColumnId: data.boardColumnId[0],
          assigneeUserId: data.assigneeId[0],
          tagIds: data.tagIds,
        }),
    });
  };

  const handleEditTask = (task: TaskModel) => {
    boardModalsStore.open("CreateOrEditTaskDialog", {
      task,
      board: boardStore.currentBoard!,
      boardColumn: column,
      onEdit: (data) =>
        tasksStore.updateTask({
          taskId: task.id,
          tenantId: organizationStore.currentOrganization!.id,
          projectId: task.projectId,
          boardId: task.boardId,
          title: data.title,
          descriptionAsJson: JSON.stringify(data.description),
          descriptionAsPlainText: extractTextFromTiptap(data.description),
          boardColumnId: data.boardColumnId[0],
          assigneeUserId: data.assigneeId[0],
          tagIds: data.tagIds,
        }),
    });
  };

  const handleDeleteTask = (task: TaskModel) => {
    sharedModalsStore.open("ConfirmModal", {
      title: "Delete Task",
      description: "Are you sure you want to delete this task? This action cannot be undone.",
      onConfirm: () =>
        tasksStore.deleteTask({
          taskId: task.id,
          tenantId: organizationStore.currentOrganization!.id,
          projectId: task.projectId,
          boardId: task.boardId,
        }),
    });
  };

  return (
    <Stack
      ref={setNodeRef}
      width="300px"
      minWidth="300px"
      height="100%"
      padding={4}
      borderWidth="2px"
      borderColor={isOver ? "blue.500" : "border.muted"}
      bg={isOver ? "blue.subtle" : "transparent"}
      transition="all 0.2s"
    >
      <Flex justifyContent="space-between">
        <Heading>{column.name}</Heading>
        <IconButton variant="outline" onClick={handleCreateTask}>
          <LuPlus />
        </IconButton>
      </Flex>

      {taskStore.tasksByColumnId[column.id]?.map((task) => (
        <TaskCard key={task.id} task={task} onEdit={handleEditTask} onDelete={handleDeleteTask} />
      ))}
    </Stack>
  );
});
