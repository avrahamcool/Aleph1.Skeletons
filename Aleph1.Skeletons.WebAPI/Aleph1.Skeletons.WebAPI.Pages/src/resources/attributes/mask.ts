import { autoinject, bindable, bindingMode, BindingEngine, TaskQueue, Disposable } from "aurelia-framework";
import { default as IMask } from "imask";

@autoinject()
export class MaskCustomAttribute
{
	constructor(element: Element, private bindingEngine: BindingEngine, private taskQueue: TaskQueue)
	{
		if (!(element instanceof HTMLInputElement))
		{
			throw new Error("Custom attribute `mask` can only be used on input elements!");
		}
		this.element = element;
	}

	@bindable({ defaultBindingMode: bindingMode.oneTime, primaryProperty: true })
	private options: string | IMask.AnyMaskedOptions;

	private element: HTMLInputElement;
	private instance: IMask.InputMask<IMask.AnyMaskedOptions>;
	private valueSyncHandler: Disposable;

	public attached()
	{
		const options = (typeof this.options === "string")
			? {
				mask: this.options
			} : this.options;

		this.instance = IMask(this.element, options);

		this.valueSyncHandler = this.bindingEngine
			.propertyObserver(this.element, "value")
			.subscribe((newValue: string) =>
			{
				if (this.instance.value !== newValue)
				{
					this.taskQueue.queueTask(() =>
					{
						this.instance.value = newValue;
						this.element.dispatchEvent(new Event("change"));
					});
				}
			});

		this.element.dispatchEvent(new Event("change"));
	}

	public detached()
	{
		this.valueSyncHandler.dispose();
		this.instance.destroy();
	}
}
