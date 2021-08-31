import { autoinject, bindable, bindingMode } from "aurelia-framework";
import * as focusTrap from "focus-trap";

@autoinject()
export class FocusTrapCustomAttribute
{
	constructor(element: Element)
	{
		if (!(element instanceof HTMLElement))
		{
			throw new Error("Custom attribute `focus-trap` can only be used on HTML elements!");
		}
		this.element = element;
	}

	@bindable({ defaultBindingMode: bindingMode.oneTime, primaryProperty: true })
	private options: Partial<focusTrap.Options>;

	private element: HTMLElement;
	private instance?: focusTrap.FocusTrap;

	public attached()
	{
		const options: Partial<focusTrap.Options> = {
			escapeDeactivates: false,
			clickOutsideDeactivates: false,
			allowOutsideClick: true,
			returnFocusOnDeactivate: true,
			...this.options
		};

		try
		{
			this.instance = focusTrap.createFocusTrap(this.element, options);
			this.instance.activate();
		}
		catch {}
	}

	public detached()
	{
		this.instance?.deactivate();
		delete this.instance;
	}
}
