import React from "react";
import { observer } from "mobx-react-lite";

import { AppSidebar, type AppSidebarNavItemProps } from "src/shared-module/layout";
import { getNavItems } from "./user-settings-sidebar.utils";

export const UserSettingsSidebar: React.FC = observer(() => {
  const [navItems, setNavItems] = React.useState<AppSidebarNavItemProps[]>([]);

  React.useEffect(() => {
    setNavItems(getNavItems());
  }, []);

  return <AppSidebar title="User Settings" navItems={navItems} />;
});
