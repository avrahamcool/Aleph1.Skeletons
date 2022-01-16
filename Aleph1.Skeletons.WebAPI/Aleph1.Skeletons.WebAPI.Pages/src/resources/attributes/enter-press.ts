import { autoinject } from "aurelia-framework";

@autoinject()
export class EnterPressCustomAttribute
{
	constructor(element: Element)
	{
		if (!(element instanceof HTMLElement))
		{
			throw new Error("Custom attribute `enter-press` can only be used on HTML elements!");
		}
		this.element = element;
	}

	private value: Function;
	private element: HTMLElement;

	private listener = (event: KeyboardEvent): boolean =>
	{
		if (event.key === "Enter")
		{
			event.preventDefault();
			event.stopPropagation();

			this.value();

			return false;
		}

		return true;
	};

	public attached()
	{
		this.element.addEventListener("keydown", this.listener);
	}

	public detached()
	{
		this.element.removeEventListener("keydown", this.listener);
	}
}
