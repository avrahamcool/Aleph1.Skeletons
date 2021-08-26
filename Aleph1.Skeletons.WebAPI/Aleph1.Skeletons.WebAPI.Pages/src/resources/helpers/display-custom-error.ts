import { AllowedErrors, customError, displayError } from "resources/helpers";

export function displayCustomError(
	error?: unknown, responses?: Partial<Record<AllowedErrors, string>> | string
): Promise<void>
{
	return displayError(customError(error, responses), error);
}
