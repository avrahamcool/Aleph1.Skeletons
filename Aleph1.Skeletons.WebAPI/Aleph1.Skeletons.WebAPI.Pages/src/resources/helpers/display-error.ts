import { Container } from "aurelia-dependency-injection";
import { getLogger } from "aurelia-logging";
import { Notyf } from "notyf";
import { UserService } from "resources/services";

const logger = getLogger("LOGGER");

export async function displayError(message: string, error: unknown): Promise<void>
{
	const notyf = Container.instance.get(Notyf);

	const userService = Container.instance.get(UserService);

	let errorsArray = [message];

	if (error instanceof Response)
	{
		if (userService.isSignedIn && error.status === 401)
		{
			await userService.signOut();
			message = "פג תוקף זמן ההתחברות שלך, אנא התחבר מחדש.";
			notyf.error(message);
			logger.error(message, error);
		}
		else
		{
			const errorText = await error.text();

			try
			{
				const errorObject = JSON.parse(errorText);

				if (error.status === 400 && errorObject.modelState)
				{
					errorsArray[0] += ":";
					errorsArray = errorsArray.concat(...Object.values(errorObject.modelState) as string[]);
				}

				logger.error(message, error.status, errorObject);
			}
			catch
			{
				logger.error(message, error.status, errorText);
			}
		}
	}
	else
	{
		logger.error(message, error);
	}

	notyf.error(errorsArray.join("<br>"));
}
