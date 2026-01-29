import React from "react";
import { observer } from "mobx-react-lite";
import { Flex } from "@chakra-ui/react";
import { DndContext, type DragEndEvent, DragOverlay, type DragStartEvent, PointerSensor, useSensor, useSensors } from "@dnd-kit/core";

import { useBoardStore, useTaskStore } from "src/board-module/store";
import { BoardColumn } from "src/board-module/pages/board-workspace/board-column";
import { ProjectSidebar } from "src/project-module/components/project-sidebar";
import { useOrganizationStore } from "src/organization-module/store";
import { useTagStore } from "src/project-module/store";
import type { TaskModel } from "src/board-module/models";
import { TaskCard } from "src/board-module/pages/board-workspace/board-column/task-card";
import { extractTextFromTiptap } from "src/board-module/utils";

const BoardWorkspace: React.FC = observer(() => {
  const organizationStore = useOrganizationStore();
  const boardStore = useBoardStore();
  const taskStore = useTaskStore();
  const tagStore = useTagStore();
  const [loadingTasks, setLoadingTasks] = React.useState(false);
  const [loadingTags, setLoadingTags] = React.useState(false);
  const [activeTask, setActiveTask] = React.useState<TaskModel | null>(null);
  // const [search, setSearch] = React.useState("");
  // const debounceRef = React.useRef<number | null>(null);

  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 8,
      },
    })
  );

  React.useEffect(() => {
    setLoadingTasks(true);
    taskStore
      .fetchTasksByBoardId({
        tenantId: organizationStore.currentOrganization!.id,
        projectId: boardStore.currentBoard!.projectId,
        boardId: boardStore.currentBoard!.id,
      })
      .catch((error) => {
        console.error("Failed to fetch tasks for the board:", error);
      })
      .finally(() => setLoadingTasks(false));
  }, [boardStore.currentBoard, organizationStore, boardStore, taskStore]);

  React.useEffect(() => {
    if (tagStore.tags.length !== 0) {
      return;
    }

    setLoadingTags(true);
    tagStore
      .fetchTagsByProjectId({
        tenantId: organizationStore.currentOrganization!.id,
        projectId: boardStore.currentBoard!.projectId,
      })
      .catch((error) => {
        console.error("Failed to fetch tags for the project:", error);
      })
      .finally(() => setLoadingTags(false));
  }, [boardStore.currentBoard, organizationStore.currentOrganization, tagStore, taskStore]);

  // const handleSearch = React.useCallback((e: React.ChangeEvent<HTMLInputElement>) => {
  //   const value = e.target.value;
  //   setSearch(value);
  //
  //   if (debounceRef.current) {
  //     window.clearTimeout(debounceRef.current);
  //   }
  //
  //   debounceRef.current = window.setTimeout(() => {
  //     taskStore.fetchTasksByBoardId({
  //       tenantId: organizationStore.currentOrganization!.id,
  //       projectId: boardStore.currentBoard!.projectId,
  //       boardId: boardStore.currentBoard!.id,
  //       search: value,
  //     });
  //   }, 300);
  // }, [boardStore.currentBoard, organizationStore.currentOrganization, taskStore]);

  const handleDragStart = (event: DragStartEvent) => {
    const task = taskStore.tasks.find((t) => t.id === event.active.id);
    if (task) {
      setActiveTask(task);
    }
  };

  const handleDragEnd = async (event: DragEndEvent) => {
    const { active, over } = event;
    setActiveTask(null);

    if (!over) return;

    const taskId = active.id as string;
    const newColumnId = over.id as string;
    const task = taskStore.tasks.find((t) => t.id === taskId);

    if (!task || task.boardColumn.id === newColumnId) {
      return;
    }

    const originalColumnId = task.boardColumn.id;
    
    // Optimistically update the task's column immediately
    taskStore.updateTaskColumnOptimistically(taskId, newColumnId);

    try {
      await taskStore.updateTask({
        taskId: task.id,
        tenantId: organizationStore.currentOrganization!.id,
        projectId: task.projectId,
        boardId: task.boardId,
        title: task.title,
        descriptionAsJson: task.description,
        descriptionAsPlainText: extractTextFromTiptap(JSON.parse(task.description)),
        boardColumnId: newColumnId,
        assigneeUserId: task.assignedTo?.userId,
        tagIds: task.tags.map((tag) => tag.tagId),
      });
    } catch (error) {
      // Rollback on error
      taskStore.updateTaskColumnOptimistically(taskId, originalColumnId);
      console.error("Failed to update task column:", error);
    }
  };

  return (
    <Flex flex="1" direction="row">
      <ProjectSidebar />

      {loadingTags || loadingTasks ? (
        <div>Loading...</div>
      ) : (
        <DndContext sensors={sensors} onDragStart={handleDragStart} onDragEnd={handleDragEnd}>
          <Flex direction="column" width="calc(100vw - 320px)" overflow="hidden" p={4}>
            <Flex flex="1" width="100%" gap={4} overflowX="auto">
              {boardStore.currentBoard!.columns.map((column) => (
                <BoardColumn key={column.id} column={column} />
              ))}
            </Flex>
          </Flex>
          <DragOverlay>
            {activeTask ? <TaskCard task={activeTask} isDragging /> : null}
          </DragOverlay>
        </DndContext>
      )}
    </Flex>
  );
});

export default BoardWorkspace;
