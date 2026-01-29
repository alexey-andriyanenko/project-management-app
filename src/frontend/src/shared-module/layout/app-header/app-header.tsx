import React from "react";
import { observer } from "mobx-react-lite";
import { useNavigate } from "react-router-dom";
import {
    Flex,
    MenuItem,
    HStack,
    Menu,
    Avatar,
} from "@chakra-ui/react";
import { ColorModeButton } from "src/shared-module/components/theme";
import { useAuthStore } from "src/auth-module/store";
import { AuthRoutes } from "src/auth-module";
import {pickColor} from "src/shared-module/utils";

export const AppHeader: React.FC = observer(() => {
  const authStore = useAuthStore();
  const navigate = useNavigate();
  const user = authStore.currentUser;

  const handleLogout = () => {
    authStore.signOut();
    navigate(AuthRoutes.login);
  };

  const handleSettings = () => {
    navigate(AuthRoutes.userSettings);
  };

  if (!user) {
    return (
      <Flex
        justifyContent="flex-end"
        alignItems="center"
        padding="16px 40px"
        borderBottom="1px solid"
        backgroundColor="gray.100"
      >
        <ColorModeButton />
      </Flex>
    );
  }

  // const userName = `${user.firstName} ${user.lastName}`;

  return (
    <Flex
      justifyContent="flex-end"
      alignItems="center"
      padding="16px 40px"
      borderBottom="1px solid"
      backgroundColor="gray.100"
      _dark={{ backgroundColor: "gray.800" }}
    >
      <HStack gap={4}>
        <ColorModeButton />
        <Menu.Root>
            <Menu.Trigger>
                <Avatar.Root colorPalette={pickColor(`${user.firstName} ${user.lastName}`)} size="sm">
                    <Avatar.Fallback name={`${user.firstName} ${user.lastName}`} />
                </Avatar.Root>
            </Menu.Trigger>

            <Menu.Positioner>
                <Menu.Content>
                    <MenuItem value="settings" onClick={handleSettings}>
                        Settings
                    </MenuItem>
                    <MenuItem value="logout" onClick={handleLogout}>
                        Logout
                    </MenuItem>
                </Menu.Content>
            </Menu.Positioner>
        </Menu.Root>
      </HStack>
    </Flex>
  );
});
