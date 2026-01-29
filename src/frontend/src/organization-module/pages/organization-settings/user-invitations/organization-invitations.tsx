import React from "react";
import { Flex, Button } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";

import {
  useModalsStore,
  useOrganizationStore,
  useUserInvitationStore,
  useOrganizationUserStore,
} from "../../../store";
import { useModalsStore as useSharedModalsStore } from "src/shared-module/store/modals";
import { InvitationsList } from "./invitations-list";

export const OrganizationInvitations: React.FC = observer(() => {
  const organizationStore = useOrganizationStore();
  const userInvitationStore = useUserInvitationStore();
  const organizationUserStore = useOrganizationUserStore();
  const modalsStore = useModalsStore();
  const sharedModalsStore = useSharedModalsStore();
  const [loading, setLoading] = React.useState(true);

  React.useEffect(() => {
    setLoading(true);
    userInvitationStore
      .fetchInvitations({ tenantId: organizationStore.currentOrganization!.id })
      .then(() => setLoading(false))
      .catch((error) => {
        console.error("Failed to fetch invitations:", error);
        setLoading(false);
      });
  }, [organizationStore.currentOrganization, userInvitationStore]);

  const handleCreateInvitation = () => {
    modalsStore.open("CreateUserInvitationDialog", {
      onCreate: (data) =>
        userInvitationStore.createInvitation({
          tenantId: organizationStore.currentOrganization!.id,
          email: data.email,
          firstName: data.firstName,
          lastName: data.lastName,
          tenantMemberRole: data.role[0],
        }),
    });
  };

  const handleResendInvitation = (invitationId: string) => {
    sharedModalsStore.open("ConfirmModal", {
      title: "Resend Invitation",
      description: "Are you sure you want to resend this invitation?",
      onConfirm: () =>
        userInvitationStore.resendInvitation({ invitationId }),
    });
  };

  const handleDeleteInvitation = (invitationId: string) => {
    const invitation = userInvitationStore.invitations.find(
      (inv) => inv.id === invitationId
    )!;

    sharedModalsStore.open("ConfirmModal", {
      title: "Delete Invitation",
      description: `Are you sure you want to delete the invitation for ${invitation.firstName} ${invitation.lastName} (${invitation.email})?`,
      onConfirm: () =>
        userInvitationStore.deleteInvitation({ invitationId }),
    });
  };

  const handleRetryMembership = (invitationId: string) => {
    const invitation = userInvitationStore.invitations.find(
      (inv) => inv.id === invitationId
    )!;

    sharedModalsStore.open("ConfirmModal", {
      title: "Retry Membership Creation",
      description: `Do you want to retry creating membership for ${invitation.firstName} ${invitation.lastName} (${invitation.email})?`,
      onConfirm: () =>
        organizationUserStore.retryMembershipCreationFromInvitation({
          tenantId: organizationStore.currentOrganization!.id,
          invitationId,
        }).then(() => {
          userInvitationStore.fetchInvitations({
            tenantId: organizationStore.currentOrganization!.id,
          });
        }),
    });
  };

  return (
    <Flex direction="column" width="100%" p={4}>
      {loading ? (
        <div>Loading invitations...</div>
      ) : (
        <>
          <Flex justify="flex-end" mb={4}>
            <Button variant="outline" onClick={handleCreateInvitation}>
              Create Invitation
            </Button>
          </Flex>

          {userInvitationStore.invitations.length === 0 ? (
            <Flex justify="center" align="center" p={8}>
              <div>No invitations yet. Create one to get started.</div>
            </Flex>
          ) : (
            <InvitationsList
              invitations={userInvitationStore.invitations}
              onResend={handleResendInvitation}
              onDelete={handleDeleteInvitation}
              onRetryMembership={handleRetryMembership}
            />
          )}
        </>
      )}
    </Flex>
  );
});
