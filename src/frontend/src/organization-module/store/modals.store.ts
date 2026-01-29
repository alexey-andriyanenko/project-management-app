import { ModalsFactory } from "src/modals-module";
import type { ModalsPropsBase, ModalsStoreRegistryGuard } from "src/modals-module";

import type {
  CreateOrEditOrganizationDialogProps,
  CreateUserInvitationDialogProps,
} from "src/organization-module/components/modals";

export type ModalName = 
  | "CreateOrEditOrganizationDialog" 
  | "CreateUserInvitationDialog";

export interface IModalsStoreRegistry extends ModalsStoreRegistryGuard<ModalName> {
  CreateOrEditOrganizationDialog: Omit<CreateOrEditOrganizationDialogProps, keyof ModalsPropsBase>;
  CreateUserInvitationDialog: Omit<CreateUserInvitationDialogProps, keyof ModalsPropsBase>;
}

export const modalsStore = ModalsFactory.createStore<ModalName, IModalsStoreRegistry>();
