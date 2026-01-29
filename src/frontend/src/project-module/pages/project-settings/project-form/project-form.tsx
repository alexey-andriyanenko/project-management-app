import React from "react";
import { observer } from "mobx-react-lite";
import {
  Button,
  Field,
  Flex,
  Input,
  Stack,
  Textarea,
} from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";

import type { ProjectFormValues } from "./project-form.types.ts";
import { useProjectStore } from "src/project-module/store";
import { toSlug } from "src/shared-module/utils/to-slug/to-slug.ts";
import { useOrganizationStore } from "src/organization-module/store";

export const ProjectForm: React.FC = observer(() => {
  const navigate = useNavigate();
  const organizationStore = useOrganizationStore();
  const projectStore = useProjectStore();
  const { formState, register, watch, handleSubmit, reset } = useForm<ProjectFormValues>({
    defaultValues: {
      name: projectStore.currentProject!.name,
      description: projectStore.currentProject!.description,
    },
  });
  const watchedName = watch("name");

  const onSubmit = handleSubmit(async (data) => {
    const slugChanged = projectStore.currentProject!.slug != toSlug(data.name);

    await projectStore.updateProject({
      projectId: projectStore.currentProject!.id,
      tenantId: organizationStore.currentOrganization!.id,
      name: data.name,
      description: data.description,
    });

    reset(data);

    if (slugChanged) {
      navigate(
        `/organization/${organizationStore.currentOrganization!.slug}/projects/${projectStore.currentProject!.slug}/settings`,
      );
    }
  });

  const onCancel = () => reset();

  return (
    <Stack width="50%" gap={4}>
      <Field.Root invalid={!!formState.errors.name}>
        <Field.Label>Project Name</Field.Label>
        <Input
          {...register("name", {
            required: {
              value: true,
              message: "Name is required",
            },
            minLength: {
              value: 2,
              message: "Project name must be at least 2 characters long",
            },
            pattern: {
              value: /^(?!.* {2,})[A-Za-z0-9 ]+$/,
              message:
                "Project name can only contain letters, numbers, and spaces, and cannot contain consecutive spaces",
            },
          })}
        />
        <Field.ErrorText>{formState.errors.name?.message}</Field.ErrorText>
      </Field.Root>

      <Field.Root>
        <Field.Label>Project URL (auto-generated)</Field.Label>
        <Input value={toSlug(watchedName)} readOnly disabled />
      </Field.Root>

      <Field.Root invalid={!!formState.errors.description}>
        <Field.Label>Project Description</Field.Label>
        <Textarea
          {...register("description", {
            maxLength: {
              value: 250,
              message: "Description cannot exceed 250 characters",
            },
          })}
        />
        <Field.ErrorText>{formState.errors.description?.message}</Field.ErrorText>
      </Field.Root>

      <Flex alignItems="center" gap={4}>
        <Button variant="outline" onClick={onCancel}>
          Cancel
        </Button>
        <Button onClick={onSubmit} disabled={!formState.isDirty} loading={formState.isSubmitting}>
          Submit
        </Button>
      </Flex>
    </Stack>
  );
});
