import { Binding, View } from "aurelia-framework";

type PatchedBinding = Binding & { originalUpdateTarget?: (value: unknown) => void };

export class AsyncBindingBehavior 
{
	private bind(binding: PatchedBinding, _view: View, busyMessage = "...") 
	{
		binding.originalUpdateTarget = binding.updateTarget;
		binding.updateTarget = (value: unknown) => 
		{
			if (value instanceof Promise) 
			{
				binding.originalUpdateTarget?.(busyMessage);
				value.then(data => binding.originalUpdateTarget?.(data));
			}
			else 
			{
				binding.originalUpdateTarget?.(value);
			}
		};
	}

	private unbind(binding: PatchedBinding) 
	{
		binding.updateTarget = binding.originalUpdateTarget;
		binding.originalUpdateTarget = undefined;
	}
}
