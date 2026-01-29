import { createListCollection } from "@chakra-ui/react";

export const scrumDefaultBoardColumns: string[] = [
  "To Do",
  "In Progress",
  "In Review",
  "Done",
];

export const kanbanDefaultBoardColumns: string[] = ["Backlog", "To Do", "In Progress", "Done"];

export const backlogDefaultBoardColumns: string[] = [];

export const boardTypeOptions = createListCollection({
  items: [
    { label: "Kanban", value: "kanban" },
    { label: "Scrum", value: "scrum" },
    { label: "Backlog", value: "backlog" },
    {},
  ],
});
