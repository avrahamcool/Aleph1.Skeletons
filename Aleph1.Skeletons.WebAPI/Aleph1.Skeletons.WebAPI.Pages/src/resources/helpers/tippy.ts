import { createSingleton, default as tippy } from "tippy.js";
import type { CreateSingletonInstance, CreateSingletonProps, Instance, Props } from "tippy.js";

export type TippyInstances<T extends string> = Record<T, Instance<Props>>;
export type TippyElements<T extends string> = Record<T, HTMLElement>;
export type TippyOptions<T extends string> = Record<T, Partial<Props>>;
export type TippySingletonOptions = Partial<CreateSingletonProps<Props>>;

export function createTippyInstances<T extends string>(
	instances: TippyInstances<T>, elements: TippyElements<T>, options?: TippyOptions<T>
)
{
	Object.entries<HTMLElement>(elements).forEach(([key, element]) =>
	{
		if (element instanceof HTMLElement)
		{
			Reflect.set(instances, key, tippy(element, options?.[key as T]));
		}
	});
}

export function createTippySingleton<T extends string>(
	instances: TippyInstances<T>, options?: TippySingletonOptions
)
{
	return createSingleton(Object.values<Instance<Props>>(instances).filter(x => !!x), options);
}

export function destroyTippyInstances<T extends string>(instances: TippyInstances<T>)
{
	Object.entries<Instance<Props>>(instances).forEach(([_, instance]) =>
	{
		if (instance)
		{
			instance.destroy();
		}
	});
}

export function destroyTippySingleton(singleton: CreateSingletonInstance<CreateSingletonProps<Props>>)
{
	if (singleton)
	{
		singleton.destroy();
	}
}
