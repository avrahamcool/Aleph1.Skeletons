/* eslint-disable class-methods-use-this */
import { Binding, View } from "aurelia-framework";

type TAsyncBinding = Binding & { originalUpdateTarget?: (value: unknown) => void };

export class AsyncBindingBehavior
{
	public bind(binding: TAsyncBinding, _view: View, busyMessage = "טוען...")
	{
		binding.originalUpdateTarget = binding.updateTarget;

		binding.updateTarget = (value: unknown) =>
		{
			if (value instanceof Promise)
			{
				binding.originalUpdateTarget!(busyMessage);
				value.then(data => binding.originalUpdateTarget!(data));
			}
			else
			{
				binding.originalUpdateTarget!(value);
			}
		};
	}

	public unbind(binding: TAsyncBinding)
	{
		binding.updateTarget = binding.originalUpdateTarget;
		binding.originalUpdateTarget = undefined;
	}
}
