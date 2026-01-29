import { Stack, Flex, Heading, Box, Text, Avatar, IconButton } from "@chakra-ui/react";
import React from "react";
import { useDraggable } from "@dnd-kit/core";
import { CSS } from "@dnd-kit/utilities";
import type { TaskModel } from "src/board-module/models";
import { pickColor } from "src/shared-module/utils";
import { TaskTag } from "src/board-module/components/task-tag";
import { LuPencil, LuTrash } from "react-icons/lu";

type TaskCardProps = {
  task: TaskModel;
  onEdit?: (task: TaskModel) => void;
  onDelete?: (task: TaskModel) => void;
  isDragging?: boolean;
};

export const TaskCard: React.FC<TaskCardProps> = ({ task, onEdit, onDelete, isDragging = false }) => {
  const { attributes, listeners, setNodeRef, transform, isDragging: isActiveDragging } = useDraggable({
    id: task.id,
    disabled: isDragging,
  });

  const style = {
    transform: CSS.Translate.toString(transform),
    cursor: isDragging ? "default" : "grab",
  };

  const handleEdit = () => onEdit?.(task);

  const handleDelete = () => onDelete?.(task);

  return (
    <Stack
      ref={setNodeRef}
      style={style}
      gap="2"
      borderWidth="1px"
      p="4"
      borderRadius="md"
      bg={isDragging ? "bg.muted" : undefined}
      shadow={isDragging ? "lg" : undefined}
      visibility={isActiveDragging ? "hidden" : "visible"}
      {...attributes}
      {...listeners}
    >
      <Heading size="sm">{task.title}</Heading>

      <Flex alignItems="center" gap="4">
        <Text>Assigned to:</Text>

        {task.assignedTo ? (
          <Avatar.Root colorPalette={pickColor(task.assignedTo.fullName)} size="sm">
            <Avatar.Fallback name={task.assignedTo.fullName} />
          </Avatar.Root>
        ) : (
          <Box ml="2" fontSize="sm" color="gray.500">
            Unassigned
          </Box>
        )}
      </Flex>

      {task.tags.length !== 0 ? (
        <Flex alignItems="center" gap="2" wrap="wrap">
          <Text>Tags:</Text>

          {task.tags.map((tag) => (
            <TaskTag key={tag.tagId} tag={tag} />
          ))}
        </Flex>
      ) : (
        <Box ml="2" fontSize="sm" color="gray.500">
          No tags
        </Box>
      )}

      {!isDragging && (
        <Flex justifyContent="flex-end" alignItems="center" gap="2">
          <IconButton variant="outline" onClick={handleEdit}>
            <LuPencil />
          </IconButton>

          <IconButton variant="outline" color="red" onClick={handleDelete}>
            <LuTrash />
          </IconButton>
        </Flex>
      )}
    </Stack>
  );
};
