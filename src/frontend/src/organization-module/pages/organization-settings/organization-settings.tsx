import React from "react";

import { Flex, Heading, Tabs } from "@chakra-ui/react";
import { LuUsers, LuMail } from "react-icons/lu";

import { OrganizationSidebar } from "src/organization-module/components/organization-sidebar";
import OrganizationUsers from "./organization-users";
import { OrganizationInvitations } from "./user-invitations";
import { OrganizationForm } from "src/organization-module/pages/organization-settings/organization-form";

const OrganizationSettings: React.FC = () => {
  return (
    <Flex flex="1" direction="row" width="100%" height="100%">
      <OrganizationSidebar />

      <Flex direction="column" width="100%" p={4} gap={4}>
        <Heading> Organization Settings </Heading>

        <OrganizationForm />

        <Tabs.Root defaultValue="users">
          <Tabs.List>
            <Tabs.Trigger value="users">
              <LuUsers />
              Users
            </Tabs.Trigger>
            <Tabs.Trigger value="invitations">
              <LuMail />
              Invitations
            </Tabs.Trigger>
          </Tabs.List>
          <Tabs.Content value="users">
            <OrganizationUsers />
          </Tabs.Content>
          <Tabs.Content value="invitations">
            <OrganizationInvitations />
          </Tabs.Content>
        </Tabs.Root>
      </Flex>
    </Flex>
  );
};

export default OrganizationSettings;
