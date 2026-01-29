import React from "react";
import { Flex, Stack, Heading, Text, Spinner, Button } from "@chakra-ui/react";
import { useNavigate, useSearchParams } from "react-router-dom";

import { invitationApiService } from "src/auth-module/api";
import { AcceptInvitationForm } from "./accept-invitation-form";
import { AuthRoutes } from "src/auth-module";

const AcceptInvitation: React.FC = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const token = searchParams.get("token");

  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState<string | null>(null);
  const [invitationData, setInvitationData] = React.useState<{
    firstName: string;
    lastName: string;
    email: string;
    role: string;
  } | null>(null);

  React.useEffect(() => {
    if (!token) {
      setError("Invalid invitation link");
      setLoading(false);
      return;
    }

    invitationApiService
      .validateInvitation({ invitationToken: token })
      .then((response) => {
        if (response.isValid) {
          setInvitationData({
            firstName: response.firstName!,
            lastName: response.lastName!,
            email: response.email!,
            role: response.tenantMemberRole!,
          });
        } else {
          setError("This invitation is invalid or has expired");
        }
      })
      .catch(() => {
        setError("Failed to validate invitation");
      })
      .finally(() => {
        setLoading(false);
      });
  }, [token]);

  const handleAcceptInvitation = async (data: {
    userName: string;
    password: string;
  }) => {
    if (!token) return;

    try {
      await invitationApiService.acceptInvitation({
        invitationToken: token,
        userName: data.userName,
        password: data.password,
      });

      navigate(AuthRoutes.login);
    } catch (error) {
      console.error("Failed to accept invitation:", error);
      setError("Failed to accept invitation. Please try again.");
    }
  };

  return (
    <Flex
      flex="1"
      direction="column"
      justify="start"
      align="center"
      padding="80px 40px 40px 40px"
    >
      <Stack width="450px" gap="10">
        <Flex justify="center">
          <Heading fontSize="4xl">Accept Invitation</Heading>
        </Flex>

        {loading ? (
          <Flex justify="center" align="center" p={8}>
            <Spinner size="lg" />
          </Flex>
        ) : error ? (
          <Stack gap={4} align="center" p={8}>
            <Text color="red.500">{error}</Text>
            <Button
              colorScheme="blue"
              onClick={() => navigate(AuthRoutes.login)}
            >
              Go to Login
            </Button>
          </Stack>
        ) : invitationData ? (
          <AcceptInvitationForm
            firstName={invitationData.firstName}
            lastName={invitationData.lastName}
            email={invitationData.email}
            role={invitationData.role}
            onSubmit={handleAcceptInvitation}
          />
        ) : null}
      </Stack>
    </Flex>
  );
};

export default AcceptInvitation;
