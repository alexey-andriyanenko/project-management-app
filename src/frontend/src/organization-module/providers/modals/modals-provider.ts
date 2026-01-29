import React from "react";
import { ModalsFactory, type ModalsProviderRegistryGuard } from "src/modals-module";

import { modalsStore } from "../../store/modals.store.ts";
import type { IModalsStoreRegistry, ModalName } from "../../store/modals.store.ts";
import {
  CreateOrEditOrganizationDialog,
  type CreateOrEditOrganizationDialogProps,
  CreateUserInvitationDialog,
  type CreateUserInvitationDialogProps,
} from "../../components/modals";

interface IModalsProviderRegistry extends ModalsProviderRegistryGuard<ModalName> {
  CreateOrEditOrganizationDialog: React.FC<CreateOrEditOrganizationDialogProps>;

  CreateUserInvitationDialog: React.FC<CreateUserInvitationDialogProps>;
}

const modalsRegistry: IModalsProviderRegistry = {
  CreateOrEditOrganizationDialog: CreateOrEditOrganizationDialog,
  CreateUserInvitationDialog: CreateUserInvitationDialog,
};

export const ModalsProvider = ModalsFactory.createProvider<
  ModalName,
  IModalsStoreRegistry,
  IModalsProviderRegistry
>(modalsStore, modalsRegistry);
