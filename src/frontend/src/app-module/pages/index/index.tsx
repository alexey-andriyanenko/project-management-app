import React from "react";
import {observer} from "mobx-react-lite";
import {useAuthStore} from "src/auth-module/store";
import {Navigate} from "react-router-dom";
import {AuthRoutes} from "src/auth-module";
import {useOrganizationStore} from "src/organization-module/store";
import {OrganizationRoutes} from "src/organization-module";

const Index: React.FC = observer(() => {
    const authStore = useAuthStore();
    const organizationStore = useOrganizationStore();

    if (!authStore.isLogged) {
        return <Navigate to={AuthRoutes.login}/>;
    }

    if (organizationStore.currentOrganization) {
        return <Navigate to={`/organization/${organizationStore.currentOrganization.slug}`} />;
    }

    return <Navigate to={OrganizationRoutes.select} />;
});

export default Index;
