import React from "react";

import { Dialog, Button, Text, Input, Field } from "@chakra-ui/react";

import type { ModalsPropsBase } from "src/modals-module";

export interface IConfirmImportantModalProps extends ModalsPropsBase {
  title: string;
  description?: string;
  confirmText: string;
  onConfirm: VoidFunction | (() => Promise<void>);
}

export const ConfirmImportantModal: React.FC<IConfirmImportantModalProps> = ({
  title,
  description,
  confirmText,
  onConfirm,
  onClose,
  isOpen,
}) => {
  const [isLoading, setIsLoading] = React.useState(false);
  const [inputValue, setInputValue] = React.useState("");

  const isConfirmDisabled = inputValue !== confirmText;

  const handleConfirm = async () => {
    if (isConfirmDisabled) return;
    
    setIsLoading(true);

    try {
      await onConfirm();
      setIsLoading(false);
    } finally {
      onClose();
    }
  };

  const handleClose = () => {
    setInputValue("");
    onClose();
  };

  return (
    <Dialog.Root lazyMount placement="center" open={isOpen}>
      <Dialog.Backdrop />
      <Dialog.Positioner>
        <Dialog.Content>
          <Dialog.Header justifyContent="center">
            <Dialog.Title>{title}</Dialog.Title>
          </Dialog.Header>
          <Dialog.Body display="flex" flexDirection="column" gap={4}>
            {description && <Text>{description}</Text>}
            <Field.Root>
              <Field.Label>
                Type <strong>{confirmText}</strong> to confirm
              </Field.Label>
              <Input
                value={inputValue}
                onChange={(e) => setInputValue(e.target.value)}
                placeholder={confirmText}
              />
            </Field.Root>
          </Dialog.Body>
          <Dialog.Footer>
            <Button onClick={handleClose}>Cancel</Button>
            <Button
              variant="outline"
              onClick={handleConfirm}
              loading={isLoading}
              disabled={isConfirmDisabled}
            >
              Confirm
            </Button>
          </Dialog.Footer>
        </Dialog.Content>
      </Dialog.Positioner>
    </Dialog.Root>
  );
};
