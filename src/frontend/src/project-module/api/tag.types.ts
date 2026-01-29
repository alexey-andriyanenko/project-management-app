import type { TagModel } from "../models/tag.ts";

export type GetTagsResponse = {
  tags: TagModel[];
};

export type CreateTagRequest = {
  tenantId: string;
  projectId: string;
  name: string;
  color: string;
};

export type CreateTagResponse = TagModel;

export type UpdateTagRequest = {
  tenantId: string;
  tagId: string;
  projectId: string;
  name: string;
  color: string;
};

export type UpdateTagResponse = TagModel;
