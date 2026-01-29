import type {JSONContent} from "@tiptap/react";

export type TaskFormValues = {
  title: string;
  description: JSONContent;
  tagIds: string[];

  // array because chakra ui select works with arrays
  boardColumnId: string[];
  assigneeId: string[];

  comments: CommentFormItem[];
};

export type CommentFormItem = {
  id?: string;
  createdByUserId?: string;
  content: JSONContent;
  createdAt?: string;
  updatedAt?: string;
};
