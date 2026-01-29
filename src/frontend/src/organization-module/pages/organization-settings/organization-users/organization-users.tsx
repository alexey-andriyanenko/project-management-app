import React from "react";

import {Flex} from "@chakra-ui/react";
import {observer} from "mobx-react-lite";

import {UsersList} from "./users-list";
import {useOrganizationStore, useOrganizationUserStore} from "../../../store";
import {useModalsStore as useSharedModalsStore} from "src/shared-module/store/modals";
import type {OrganizationUserModel} from "src/organization-module/models/organization-user.ts";

const OrganizationUsers: React.FC = observer(() => {
    const organizationStore = useOrganizationStore();
    const organizationUserStore = useOrganizationUserStore();
    const sharedModalsStore = useSharedModalsStore();
    const [loading, setLoading] = React.useState(true);

    React.useEffect(() => {
        organizationUserStore
            .fetchManyUsers({tenantId: organizationStore.currentOrganization!.id})
            .then(() => setLoading(false))
            .catch((error) => {
                console.error("Failed to fetch organization users:", error);
                setLoading(false);
            });
    }, [organizationStore.currentOrganization, organizationUserStore]);

    const handleDeleteUser = (user: OrganizationUserModel) => {
        sharedModalsStore.open("ConfirmModal", {
            title: `Are you sure you want to remove this user from current organization (${organizationStore.currentOrganization?.name})?`,
            description: `User: ${user.firstName} ${user.lastName} (${user.email})`,
            onConfirm: () =>
                organizationUserStore.removeUser({
                    id: user.userId,
                    tenantId: organizationStore.currentOrganization!.id,
                }),
        });
    };

    return (
        <Flex flex="1" direction="column" width="100%" gap={4}>
            <Flex direction="column" width="100%" p={4} gap={4}>
                {loading ? (
                    <div>Loading users...</div>
                ) : (
                    <UsersList
                        users={organizationUserStore.users}
                        onDelete={handleDeleteUser}
                    />
                )}
            </Flex>
        </Flex>
    );
});

export default OrganizationUsers;
