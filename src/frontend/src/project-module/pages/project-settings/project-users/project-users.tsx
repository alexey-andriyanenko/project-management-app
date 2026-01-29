import React from "react";
import {observer} from "mobx-react-lite";

import {Flex, Button} from "@chakra-ui/react";

import {useModalsStore, useProjectStore, useProjectUserStore} from "src/project-module/store";
import {useModalsStore as useSharedModalsStore} from "src/shared-module/store/modals";
import type {ProjectUserModel} from "src/project-module/models";
import {useOrganizationStore} from "src/organization-module/store";
import {UsersList} from "./users-list";

export const ProjectUsers: React.FC = observer(() => {
    const organizationStore = useOrganizationStore();
    const projectStore = useProjectStore();
    const projectUserStore = useProjectUserStore();
    const modalsStore = useModalsStore();
    const sharedModalsStore = useSharedModalsStore();

    React.useEffect(() => {
        projectUserStore
            .fetchManyProjectUsers({
                tenantId: organizationStore.currentOrganization!.id,
                projectId: projectStore.currentProject!.id,
            })
            .then(() => setLoading(false))
            .catch((error) => {
                console.error("Failed to fetch projects:", error);
                setLoading(false);
            });
    }, [organizationStore.currentOrganization, projectStore.currentProject, projectUserStore]);

    const [loading, setLoading] = React.useState(true);

    const handleAssign = async () => {
        modalsStore.open("AddUsersToProjectDialog", {
            organization: organizationStore.currentOrganization!,
            addedUsers: projectUserStore.users,
            onAssign: (data) =>
                projectUserStore.addManyUsersToProject({
                    tenantId: organizationStore.currentOrganization!.id,
                    projectId: projectStore.currentProject!.id,
                    members: data,
                }),
        });
    };

    const handleUnassign = (user: ProjectUserModel) => {
        sharedModalsStore.open("ConfirmModal", {
            title: "Are you sure you want to unassign this user?",
            description: `User: ${user.firstName} ${user.lastName}`,
            onConfirm: () =>
                projectUserStore.removeProjectUser({
                    tenantId: organizationStore.currentOrganization!.id,
                    projectId: projectStore.currentProject!.id,
                    id: user.userId,
                }),
        });
    };

    return (
        <Flex direction="column" width="100%" p={4}>
            {loading ? (
                <div>loading users...</div>
            ) : (
                <>
                    <Flex justify="flex-end">
                        <Button variant="outline" onClick={handleAssign}>
                            Add User
                        </Button>
                    </Flex>

                    <UsersList users={projectUserStore.users} onUnassign={handleUnassign}/>
                </>
            )}
        </Flex>
    );
});
