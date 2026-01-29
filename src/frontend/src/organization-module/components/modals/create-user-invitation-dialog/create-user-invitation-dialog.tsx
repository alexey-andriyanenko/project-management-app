import React from "react";
import { Controller, useForm } from "react-hook-form";

import {
  Button,
  createListCollection,
  Dialog,
  Field,
  Input,
  Portal,
  Select,
  Stack,
} from "@chakra-ui/react";
import type { ModalsPropsBase } from "src/modals-module";
import {
  OrganizationUserRole,
  OrganizationUserRoleToNameMap,
} from "src/organization-module/models/organization-user-role.ts";

type UserInvitationFormModel = {
  firstName: string;
  lastName: string;
  email: string;
  role: string[];
};

export type CreateUserInvitationDialogProps = ModalsPropsBase & {
  onCreate?: (data: UserInvitationFormModel) => Promise<void>;
};

export const CreateUserInvitationDialog: React.FC<
  CreateUserInvitationDialogProps
> = ({ isOpen, onClose, onCreate }) => {
  const { formState, control, register, handleSubmit } =
    useForm<UserInvitationFormModel>({
      defaultValues: {
        firstName: "",
        lastName: "",
        email: "",
        role: [OrganizationUserRole.Member.toString()],
      },
    });

  const collection = React.useMemo(
    () =>
      createListCollection({
        items: Object.entries(OrganizationUserRoleToNameMap).map(
          ([value, label]) => ({
            label,
            value,
          })
        ),
      }),
    []
  );

  const onSubmit = handleSubmit(async (data) => {
    await onCreate?.(data);
    onClose();
  });

  return (
    <Dialog.Root lazyMount placement="center" open={isOpen}>
      <Portal>
        <Dialog.Backdrop />
        <Dialog.Positioner>
          <Dialog.Content>
            <Dialog.Header>
              <Dialog.Title>Create User Invitation</Dialog.Title>
            </Dialog.Header>
            <Dialog.Body pb="4">
              <Stack gap="4">
                <Field.Root invalid={!!formState.errors.firstName}>
                  <Field.Label>First Name</Field.Label>
                  <Input
                    placeholder="Enter first name"
                    {...register("firstName", {
                      required: {
                        value: true,
                        message: "First name is required",
                      },
                      maxLength: {
                        value: 200,
                        message: "First name cannot exceed 200 characters",
                      },
                    })}
                  />
                  <Field.ErrorText>
                    {formState.errors.firstName?.message}
                  </Field.ErrorText>
                </Field.Root>

                <Field.Root invalid={!!formState.errors.lastName}>
                  <Field.Label>Last Name</Field.Label>
                  <Input
                    placeholder="Enter last name"
                    {...register("lastName", {
                      required: {
                        value: true,
                        message: "Last name is required",
                      },
                      maxLength: {
                        value: 200,
                        message: "Last name cannot exceed 200 characters",
                      },
                    })}
                  />
                  <Field.ErrorText>
                    {formState.errors.lastName?.message}
                  </Field.ErrorText>
                </Field.Root>

                <Field.Root invalid={!!formState.errors.email}>
                  <Field.Label>Email</Field.Label>
                  <Input
                    placeholder="Enter email"
                    {...register("email", {
                      required: {
                        value: true,
                        message: "Email is required",
                      },
                      pattern: {
                        value:
                          /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
                        message: "Invalid email address",
                      },
                    })}
                  />
                  <Field.ErrorText>
                    {formState.errors.email?.message}
                  </Field.ErrorText>
                </Field.Root>

                <Field.Root invalid={!!formState.errors.role}>
                  <Field.Label>Role</Field.Label>
                  <Controller
                    control={control}
                    name="role"
                    render={({ field }) => (
                      <Select.Root
                        name={field.name}
                        value={field.value}
                        onValueChange={(item) => field.onChange(item.value)}
                        onInteractOutside={() => field.onBlur()}
                        collection={collection}
                      >
                        <Select.HiddenSelect />
                        <Select.Control>
                          <Select.Trigger>
                            <Select.ValueText placeholder="Select role" />
                          </Select.Trigger>
                          <Select.IndicatorGroup>
                            <Select.Indicator />
                          </Select.IndicatorGroup>
                        </Select.Control>
                        <Select.Positioner>
                          <Select.Content>
                            {collection.items.map((item) => (
                              <Select.Item key={item.value} item={item}>
                                {item.label}
                                <Select.ItemIndicator />
                              </Select.Item>
                            ))}
                          </Select.Content>
                        </Select.Positioner>
                      </Select.Root>
                    )}
                  />
                  <Field.ErrorText>
                    {formState.errors.role?.message}
                  </Field.ErrorText>
                </Field.Root>
              </Stack>
            </Dialog.Body>
            <Dialog.Footer>
              <Dialog.ActionTrigger asChild>
                <Button variant="outline" onClick={onClose}>
                  Cancel
                </Button>
              </Dialog.ActionTrigger>
              <Button loading={formState.isSubmitting} onClick={onSubmit}>
                Send Invitation
              </Button>
            </Dialog.Footer>
          </Dialog.Content>
        </Dialog.Positioner>
      </Portal>
    </Dialog.Root>
  );
};
