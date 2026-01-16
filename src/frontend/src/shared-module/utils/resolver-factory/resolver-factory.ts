import type {FieldError, FieldErrors, FieldValues, Resolver, ResolverResult} from "react-hook-form";
import {Validator} from "fluentvalidation-ts";

export function resolverFactory<TFieldValues extends FieldValues>(
    validator: Validator<TFieldValues>,
): Resolver<TFieldValues> {
    return async (values: TFieldValues) => {
        const result = validator.validate(values);
        const errors: FieldErrors<TFieldValues> = {} as FieldErrors<TFieldValues>;

        for (const name in result) {
            const message = result[name];
            if (!message) continue;

            const key = name as keyof TFieldValues;
            // @ts-expect-error - react-hook-form FieldError type mismatch
            errors[key] = {type: "manual", message} as unknown as FieldError;
        }

        const hasErrors = Object.keys(errors).length > 0;

        return hasErrors
            ? ({values: {} as never, errors} as ResolverResult<TFieldValues>)
            : ({values: values as never, errors} as ResolverResult<TFieldValues>);
    };
}