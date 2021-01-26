type IMethodDecorator<T> = (target: T, propertyKey: string, descriptor: PropertyDescriptor) => void;

export function busyTracking<T>(isBusyPropertyName?: string): IMethodDecorator<T>
{
	return function (_target: T, propertyKey: string, descriptor: PropertyDescriptor)
	{
		const givenFunc: (...params: unknown[]) => Promise<unknown> = descriptor.value;
		if (typeof givenFunc !== "function")
		{
			throw Error(`'busyTracking' Decorator should be used on functions only! you used it on [${ propertyKey }]`);
		}

		isBusyPropertyName = isBusyPropertyName || `${ propertyKey }_isBusy`;
		// changing the given function by altering it's descriptor value
		// using "function" syntax to take advantage of correct "this" scope
		descriptor.value = function (...params: unknown[])
		{
			// increments counter takes care of "NaN" of first time running
			this[isBusyPropertyName] = (this[isBusyPropertyName] || 0) + 1;
			const originalRetVal: Promise<unknown> = givenFunc.call(this, ...params);
			if (!(originalRetVal instanceof Promise))
			{
				throw Error(`you used 'busyTracking' on a function that is not returning a promise [${ propertyKey }]`);
			}
			return originalRetVal.finally(() => this[isBusyPropertyName]--);
		};
	};
}