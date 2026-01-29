import React from "react";
import { observer } from "mobx-react-lite";
import { useForm } from "react-hook-form";
import { Box, Container, Heading, VStack, Input, Button, Stack, Field, Flex } from "@chakra-ui/react";
import { useAuthStore } from "src/auth-module/store";
import { UserSettingsSidebar } from "src/auth-module/components/user-settings-sidebar";

type UserFormValues = {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
};

const UserSettings: React.FC = observer(() => {
  const authStore = useAuthStore();
  const user = authStore.currentUser;

  const { formState, register, handleSubmit } = useForm<UserFormValues>({
    defaultValues: {
      firstName: user?.firstName || "",
      lastName: user?.lastName || "",
      userName: user?.userName || "",
      email: user?.email || "",
    },
  });

  const onSubmit = handleSubmit(async (data) => {
    try {
      await authStore.updateMe(data);
    } catch (error) {
      console.error("Failed to update user settings:", error);
    }
  });

  if (!user) {
    return null;
  }

  return (
    <Flex direction="row" width="100%" height="100%">
      <UserSettingsSidebar />

      <Box width="100%" height="100%" overflow="auto">
        <Container maxW="container.md" py={8}>
          <VStack align="stretch" gap={6}>
            <Heading size="lg">User Settings</Heading>
            
            <Box borderWidth="1px" borderRadius="lg" p={6}>
              <form onSubmit={onSubmit}>
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
                        minLength: {
                          value: 2,
                          message: "First name must be at least 2 characters long",
                        },
                      })}
                    />
                    <Field.ErrorText>{formState.errors.firstName?.message}</Field.ErrorText>
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
                        minLength: {
                          value: 2,
                          message: "Last name must be at least 2 characters long",
                        },
                      })}
                    />
                    <Field.ErrorText>{formState.errors.lastName?.message}</Field.ErrorText>
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
                        minLength: {
                          value: 3,
                          message: "Username must be at least 3 characters long",
                        },
                        pattern: {
                          value: /^[a-zA-Z0-9_]+$/,
                          message: "Username can only contain letters, numbers, and underscores",
                        },
                      })}
                    />
                    <Field.ErrorText>{formState.errors.userName?.message}</Field.ErrorText>
                  </Field.Root>

                  <Field.Root invalid={!!formState.errors.email}>
                    <Field.Label>Email</Field.Label>
                    <Input
                      placeholder="Enter email"
                      type="email"
                      {...register("email", {
                        required: {
                          value: true,
                          message: "Email is required",
                        },
                        pattern: {
                          value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                          message: "Invalid email address",
                        },
                      })}
                    />
                    <Field.ErrorText>{formState.errors.email?.message}</Field.ErrorText>
                  </Field.Root>

                  <Button type="submit" loading={formState.isSubmitting} alignSelf="flex-start">
                    Save Changes
                  </Button>
                </Stack>
              </form>
            </Box>
          </VStack>
        </Container>
      </Box>
    </Flex>
  );
});

export default UserSettings;
