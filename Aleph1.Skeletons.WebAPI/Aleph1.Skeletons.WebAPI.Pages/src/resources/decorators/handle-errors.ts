import { displayError } from "resources/helpers";

export function handleErrors(message: string): MethodDecorator
{
	return function decoratorInstance(target, property, descriptor: PropertyDescriptor)
	{
		if (typeof descriptor.value !== "function")
		{
			throw new Error(`Decorator 'handleErrors' was used on '${String(property)}', which is not a function.`);
		}

		const originalMethod = descriptor.value;

		descriptor.value = function patch(...args: any[]): any
		{
			try
			{
				const result = originalMethod.apply(this, args);

				if (result instanceof Promise)
				{
					return result.catch(error => displayError(message, error));
				}

				return result;
			}
			catch (error)
			{
				displayError(message, error);
				throw error;
			}
		};
	};
}
