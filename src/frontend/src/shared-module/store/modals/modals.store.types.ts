import type {IConfirmModalProps, IConfirmImportantModalProps} from "src/shared-module/components/modals";

import type {ModalsPropsBase, ModalsStoreRegistryGuard} from "src/modals-module";

export type ModalName = "ConfirmModal" | "ConfirmImportantModal";

export interface IModalsStoreRegistry extends ModalsStoreRegistryGuard<ModalName> {
    ConfirmModal: Omit<IConfirmModalProps, keyof ModalsPropsBase>;
    ConfirmImportantModal: Omit<IConfirmImportantModalProps, keyof ModalsPropsBase>;
}
