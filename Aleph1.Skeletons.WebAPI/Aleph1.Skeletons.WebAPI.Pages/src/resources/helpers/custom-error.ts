export type AllowedErrors = 400 | 401 | 402 | 403 | 404 | 405 | 500;

export function customError(error?: unknown, responses?: Partial<Record<AllowedErrors, string>> | string): string
{
	if (typeof error === "string" && error)
	{
		return error;
	}

	if (typeof responses === "string" && responses)
	{
		return responses;
	}

	const generalError = "התרחשה שגיאה בעת ההתקשרות לשרת";

	if (error instanceof Response)
	{
		return responses?.[error.status as AllowedErrors] ?? generalError;
	}

	if (error instanceof Error && error.message)
	{
		switch (error.name)
		{
			case "TypeError":
				return "התקשורת לשרת נכשלה";

			default:
				return error.message;
		}
	}

	return generalError;
}
