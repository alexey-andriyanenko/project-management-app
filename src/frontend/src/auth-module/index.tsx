import type { RouteItem } from "src/routes-module/routes-list/routes-list.types.ts";

import SignIn from "src/auth-module/sign-in";
import SignUp from "src/auth-module/sign-up";
import UserSettings from "src/auth-module/pages/user-settings";
import AcceptInvitation from "src/auth-module/pages/accept-invitation";

export const AuthRoutes = {
  login: "/auth/login",
  register: "/auth/register",
  userSettings: "/user/settings",
  acceptInvitation: "/invitations/accept",
};

const authRoutes: RouteItem[] = [
  {
    path: AuthRoutes.login,
    element: <SignIn />,
  },
  {
    path: AuthRoutes.register,
    element: <SignUp />,
  },
  {
    path: AuthRoutes.userSettings,
    element: <UserSettings />,
    isPrivate: true,
  },
  {
    path: AuthRoutes.acceptInvitation,
    element: <AcceptInvitation />,
  },
];

export default authRoutes;
