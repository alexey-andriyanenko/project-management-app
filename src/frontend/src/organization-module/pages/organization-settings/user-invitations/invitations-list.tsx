import React from "react";
import { Table, Badge, Button, Flex, HStack } from "@chakra-ui/react";
import { LuRefreshCw, LuTrash2, LuCopy, LuTriangleAlert, LuUserPlus } from "react-icons/lu";
import type { UserInvitationModel } from "src/organization-module/models/user-invitation.ts";
import { UserInvitationStatus, MembershipCreationStatus } from "src/organization-module/models/user-invitation.ts";

interface InvitationsListProps {
  invitations: UserInvitationModel[];
  onResend: (invitationId: string) => void;
  onDelete: (invitationId: string) => void;
  onRetryMembership: (invitationId: string) => void;
}

export const InvitationsList: React.FC<InvitationsListProps> = ({
  invitations,
  onResend,
  onDelete,
  onRetryMembership,
}) => {
  const getStatusColor = (status: UserInvitationStatus) => {
    switch (status) {
      case UserInvitationStatus.Pending:
        return "blue";
      case UserInvitationStatus.Accepted:
        return "green";
      case UserInvitationStatus.Expired:
        return "red";
      default:
        return "gray";
    }
  };

  const getStatusLabel = (status: UserInvitationStatus) => {
    switch (status) {
      case UserInvitationStatus.Pending:
        return "Pending";
      case UserInvitationStatus.Accepted:
        return "Accepted";
      case UserInvitationStatus.Expired:
        return "Expired";
      default:
        return "Unknown";
    }
  };

  const getMembershipStatusColor = (status: MembershipCreationStatus) => {
    switch (status) {
      case MembershipCreationStatus.NotApplicable:
        return "gray";
      case MembershipCreationStatus.Pending:
        return "orange";
      case MembershipCreationStatus.Created:
        return "green";
      default:
        return "gray";
    }
  };

  const getMembershipStatusLabel = (status: MembershipCreationStatus) => {
    switch (status) {
      case MembershipCreationStatus.NotApplicable:
        return "N/A";
      case MembershipCreationStatus.Pending:
        return "Pending";
      case MembershipCreationStatus.Created:
        return "Created";
      default:
        return "Unknown";
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  const handleCopyLink = async (link: string) => {
    try {
      await navigator.clipboard.writeText(link);
      // Optional: Show success toast/notification
    } catch (error) {
      console.error("Failed to copy link:", error);
    }
  };

  const shouldShowCopyButton = (invitation: UserInvitationModel) => {
    return (
      invitation.status === UserInvitationStatus.Pending ||
      invitation.membershipStatus === MembershipCreationStatus.Pending
    );
  };

  const shouldShowResendButton = (invitation: UserInvitationModel) => {
    return invitation.status === UserInvitationStatus.Pending;
  };

  const shouldShowRetryMembershipButton = (invitation: UserInvitationModel) => {
    return invitation.membershipStatus === MembershipCreationStatus.Pending;
  };

  return (
    <Table.Root variant="outline">
      <Table.Header>
        <Table.Row>
          <Table.ColumnHeader>Name</Table.ColumnHeader>
          <Table.ColumnHeader>Email</Table.ColumnHeader>
          <Table.ColumnHeader>Role</Table.ColumnHeader>
          <Table.ColumnHeader>Status</Table.ColumnHeader>
          <Table.ColumnHeader>Membership</Table.ColumnHeader>
          <Table.ColumnHeader>Created</Table.ColumnHeader>
          <Table.ColumnHeader>Expires</Table.ColumnHeader>
          <Table.ColumnHeader>Actions</Table.ColumnHeader>
        </Table.Row>
      </Table.Header>
      <Table.Body>
        {invitations.map((invitation) => (
          <Table.Row key={invitation.id}>
            <Table.Cell>
              {invitation.firstName} {invitation.lastName}
            </Table.Cell>
            <Table.Cell>{invitation.email}</Table.Cell>
            <Table.Cell>{invitation.tenantMemberRole}</Table.Cell>
            <Table.Cell>
              <Badge colorPalette={getStatusColor(invitation.status)}>
                {getStatusLabel(invitation.status)}
              </Badge>
            </Table.Cell>
            <Table.Cell>
              <HStack gap={2}>
                <Badge colorPalette={getMembershipStatusColor(invitation.membershipStatus)}>
                  {getMembershipStatusLabel(invitation.membershipStatus)}
                </Badge>
                {invitation.membershipStatus === MembershipCreationStatus.Pending && (
                  <LuTriangleAlert color="orange" title="Membership creation failed - user can retry by accepting invitation again" />
                )}
              </HStack>
            </Table.Cell>
            <Table.Cell>{formatDate(invitation.createdAt)}</Table.Cell>
            <Table.Cell>{formatDate(invitation.expiresAt)}</Table.Cell>
            <Table.Cell>
              <Flex gap={2}>
                {shouldShowCopyButton(invitation) && (
                  <Button
                    size="sm"
                    variant="ghost"
                    onClick={() => handleCopyLink(invitation.invitationLink)}
                    title="Copy invitation link"
                  >
                    <LuCopy />
                  </Button>
                )}
                {shouldShowResendButton(invitation) && (
                  <Button
                    size="sm"
                    variant="ghost"
                    onClick={() => onResend(invitation.id)}
                    title="Resend invitation"
                  >
                    <LuRefreshCw />
                  </Button>
                )}
                {shouldShowRetryMembershipButton(invitation) && (
                  <Button
                    size="sm"
                    variant="ghost"
                    colorPalette="green"
                    onClick={() => onRetryMembership(invitation.id)}
                    title="Retry membership creation"
                  >
                    <LuUserPlus />
                  </Button>
                )}
                <Button
                  size="sm"
                  variant="ghost"
                  colorPalette="red"
                  onClick={() => onDelete(invitation.id)}
                  title="Delete invitation"
                >
                  <LuTrash2 />
                </Button>
              </Flex>
            </Table.Cell>
          </Table.Row>
        ))}
      </Table.Body>
    </Table.Root>
  );
};

