export function busyTracking(name?: string): MethodDecorator
{
	return function decoratorInstance(target, property, descriptor: PropertyDescriptor)
	{
		if (typeof descriptor.value !== "function")
		{
			throw new Error(`Decorator 'busyTracking' was used on '${String(property)}', which is not a function.`);
		}

		const originalMethod = descriptor.value;
		const pendingPropertyName = name || `${String(property)}_pending`;

		Reflect.set(target, pendingPropertyName, 0);

		// Add 'finally' to the decorated method by altering its' descriptor value,
		// using 'function' syntax to take advantage of correct 'this' scope
		descriptor.value = function patch(...args: any[]): any
		{
			// Increments counter and also takes care of the first time running
			Reflect.set(this, pendingPropertyName, (Reflect.get(this, pendingPropertyName) || 0) + 1);

			const result = originalMethod.apply(this, args);
			if (!(result instanceof Promise))
			{
				throw new Error("Decorator 'busyTracking' should only be used on functions that return a promise.");
			}

			return result.finally(() =>
			{
				Reflect.set(this, pendingPropertyName, Reflect.get(this, pendingPropertyName) - 1);
			});
		};
	};
}
