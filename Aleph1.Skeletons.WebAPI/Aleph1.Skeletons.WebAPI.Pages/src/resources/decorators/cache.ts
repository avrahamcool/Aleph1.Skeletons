export const Forced = Symbol("Forced");

export function cache(): MethodDecorator
{
	return function decorator(target, property, descriptor: PropertyDescriptor)
	{
		if (typeof descriptor.value !== "function")
		{
			throw new Error(`Decorator 'cache' was used on '${String(property)}', which is not a function.`);
		}

		const originalMethod = descriptor.value;

		// Caching results of previous calls in a map
		const results = new Map<string, unknown>();

		// Caching the results by altering the method's descriptor value,
		// using 'function' syntax to take advantage of correct 'this' scope
		descriptor.value = function patch(...args: any[]): any
		{
			const isForced = args.find(arg => arg === Forced);
			const resultKey = JSON.stringify(args.filter(arg => arg && arg !== Forced));

			if (!results.has(resultKey) || isForced)
			{
				const result = originalMethod.apply(this, args);
				results.set(resultKey, result);
				if (result instanceof Promise)
				{
					result.catch(error =>
					{
						results.delete(resultKey);
						throw error;
					});
				}
			}

			return results.get(resultKey)!;
		};
	};
}
