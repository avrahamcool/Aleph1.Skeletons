import { second } from "../services/time";
import { getLogger } from "aurelia-logging";
import { Notyf } from "notyf";

const logger = getLogger("handleErrors");

const displayError = (friendlyError: string, realError: unknown) =>
{
	const notyfInstance = new Notyf({
		duration: 5 * second,
		dismissible: true
	});

	if (realError instanceof Response)
	{
		realError.text()
			.then(bodyAsText =>
			{
				try { return JSON.parse(bodyAsText); }
				catch { return bodyAsText; }
			})
			.then(errorObj =>
			{
				let errorsArray = [friendlyError];
				if (realError.status === 400 && errorObj.modelState)
				{
					errorsArray = errorsArray.concat(...Object.values(errorObj.modelState) as string[]);
				}

				notyfInstance.error(errorsArray.join("<br>"));
				logger.error(friendlyError, realError.status, errorObj);
			});
	}
	else
	{
		notyfInstance.error(friendlyError);
		logger.error(friendlyError, realError);
	}
};

export function handleErrors(friendlyError: string)
{
	return function (_target: unknown, _propertyKey: string, descriptor: PropertyDescriptor): PropertyDescriptor
	{
		const givenFunc = descriptor.value;
		if (typeof givenFunc !== "function")
			throw new Error("handleErrors Decorator should be used on function only!");
		descriptor.value = function (...params: readonly unknown[])
		{
			try
			{
				const retVal: unknown = givenFunc.call(this, ...params);
				if (retVal instanceof Promise)
				{
					return retVal
						.catch(e => displayError(friendlyError, e));
				}
				return retVal;
			} catch (e)
			{
				displayError(friendlyError, e);
			}
		};

		return descriptor;
	};
}
