import type { IConfirmModalProps } from "src/shared-module/components/modals";

import type { ModalsPropsBase, ModalsStoreRegistryGuard } from "src/modals-module";

export type ModalName = "ConfirmModal";

export interface IModalsStoreRegistry extends ModalsStoreRegistryGuard<ModalName> {
  ConfirmModal: Omit<IConfirmModalProps, keyof ModalsPropsBase>;
}
