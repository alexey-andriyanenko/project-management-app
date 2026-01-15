import { IApiError } from "src/shared-module/models";

export const isApiError = (error: unknown): error is IApiError => {
  if (error && typeof error === "object") {
    return (
      Object.hasOwn(error as object, "type") &&
      Object.hasOwn(error as object, "message") &&
      Object.hasOwn(error as object, "errors")
    );
  }
  return false;
};
