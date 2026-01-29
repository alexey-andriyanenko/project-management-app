import React from "react";
import { Stack, Field, Input, Button, Text } from "@chakra-ui/react";
import { useForm } from "react-hook-form";
import { observer } from "mobx-react-lite";

import type { AcceptInvitationFormValues } from "./accept-invitation-form.types.ts";

interface AcceptInvitationFormProps {
  firstName: string;
  lastName: string;
  email: string;
  role: string;
  onSubmit: (data: AcceptInvitationFormValues) => Promise<void>;
}

export const AcceptInvitationForm: React.FC<AcceptInvitationFormProps> = observer(
  ({ firstName, lastName, email, role, onSubmit }) => {
    const { register, formState, handleSubmit } = useForm<AcceptInvitationFormValues>({
      defaultValues: {
        userName: "",
        password: "",
        confirmPassword: "",
      },
    });

    return (
      <form onSubmit={handleSubmit(onSubmit)}>
        <Stack width="100%" gap="4">
          <Text>
            You've been invited to join as <strong>{role}</strong>
          </Text>

          <Field.Root>
            <Field.Label>First Name</Field.Label>
            <Input value={firstName} disabled />
          </Field.Root>

          <Field.Root>
            <Field.Label>Last Name</Field.Label>
            <Input value={lastName} disabled />
          </Field.Root>

          <Field.Root>
            <Field.Label>Email</Field.Label>
            <Input value={email} disabled />
          </Field.Root>

          <Field.Root>
            <Field.Label>Role</Field.Label>
            <Input value={role} disabled />
          </Field.Root>

          <Field.Root invalid={!!formState.errors.userName}>
            <Field.Label>Username</Field.Label>
            <Input
              placeholder="Enter username"
              {...register("userName", {
                required: {
                  value: true,
                  message: "Username is required",
                },
                maxLength: {
                  value: 100,
                  message: "Username cannot exceed 100 characters",
                },
                pattern: {
                  value: /^(?!\d)[A-Za-z0-9_]+$/,
                  message:
                    "Username can contain only letters, numbers, and underscores, and cannot start with a number",
                },
              })}
            />
            <Field.ErrorText>{formState.errors.userName?.message}</Field.ErrorText>
          </Field.Root>

          <Field.Root invalid={!!formState.errors.password}>
            <Field.Label>Password</Field.Label>
            <Input
              type="password"
              placeholder="Enter password"
              {...register("password", {
                required: {
                  value: true,
                  message: "Password is required",
                },
                pattern: {
                  value: /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$/,
                  message:
                    "Password must contain at least 8 characters, include 1 special character, 1 digit, 1 lowercase and 1 uppercase character.",
                },
              })}
            />
            <Field.ErrorText>{formState.errors.password?.message}</Field.ErrorText>
          </Field.Root>

          <Field.Root invalid={!!formState.errors.confirmPassword}>
            <Field.Label>Confirm Password</Field.Label>
            <Input
              type="password"
              placeholder="Confirm password"
              {...register("confirmPassword", {
                required: {
                  value: true,
                  message: "Confirm password is required",
                },
                validate: (value, formValues) =>
                  value === formValues.password || "Passwords do not match",
              })}
            />
            <Field.ErrorText>
              {formState.errors.confirmPassword?.message}
            </Field.ErrorText>
          </Field.Root>

          <Button loading={formState.isSubmitting} type="submit">
            Accept Invitation
          </Button>
        </Stack>
      </form>
    );
  }
);
