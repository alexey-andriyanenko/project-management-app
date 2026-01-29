import React from "react";
import {ModalsFactory, type ModalsProviderRegistryGuard} from "src/modals-module";

import type {IModalsStoreRegistry, ModalName} from "src/shared-module/store/modals";
import {
    type IConfirmModalProps,
    ConfirmModal,
    type IConfirmImportantModalProps,
    ConfirmImportantModal
} from "src/shared-module/components/modals";
import {modalsStore} from "src/shared-module/store/modals/modals.store";

interface IModalsProviderRegistry extends ModalsProviderRegistryGuard<ModalName> {
    ConfirmModal: React.FC<IConfirmModalProps>;
    ConfirmImportantModal: React.FC<IConfirmImportantModalProps>;
}

const modalsProviderRegistry: IModalsProviderRegistry = {
    ConfirmModal,
    ConfirmImportantModal,
};

export const ModalsProvider = ModalsFactory.createProvider<
    ModalName,
    IModalsStoreRegistry,
    IModalsProviderRegistry
>(modalsStore, modalsProviderRegistry);
