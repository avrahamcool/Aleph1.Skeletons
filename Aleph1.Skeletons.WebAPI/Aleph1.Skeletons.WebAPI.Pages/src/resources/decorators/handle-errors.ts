import { second } from "../services/time";
import { getLogger } from "aurelia-logging";
import { Notyf } from "notyf";

type IMethodDecorator<T> = (target: T, propertyKey: string, descriptor: PropertyDescriptor) => void;


const logger = getLogger("handleErrors");
export function displayError(friendlyError: string, realError?: unknown): void
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
}

export function handleErrors<T>(friendlyError: string): IMethodDecorator<T>
{
	return function (_target: T, propertyKey: string, descriptor: PropertyDescriptor)
	{
		const givenFunc = descriptor.value;
		if (typeof givenFunc !== "function")
		{
			throw Error(`'handleErrors' Decorator should be used on functions only! you used it on [${ propertyKey }]`);
		}

		descriptor.value = function (...params: unknown[])
		{
			try
			{
				const retVal: unknown = givenFunc.call(this, ...params);
				if (retVal instanceof Promise)
				{
					return retVal.catch(e => displayError(friendlyError, e));
				}
				return retVal;
			}
			catch (e)
			{
				displayError(friendlyError, e);
			}
		};
	};
}
