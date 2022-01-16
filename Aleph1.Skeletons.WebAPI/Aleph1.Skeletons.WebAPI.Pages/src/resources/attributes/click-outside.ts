import { autoinject, PLATFORM, TaskQueue } from "aurelia-framework";

@autoinject()
export class ClickOutsideCustomAttribute
{
	constructor(element: Element, private taskQueue: TaskQueue)
	{
		if (!(element instanceof HTMLElement))
		{
			throw new Error("Custom attribute `click-outside` can only be used on HTML elements!");
		}
		this.element = element;
	}

	private value: Function | undefined;
	private element: HTMLElement;

	private listener = (event: Event): boolean =>
	{
		if (this.element instanceof HTMLElement && event.target instanceof HTMLElement)
		{
			if (!this.element.contains(event.target) && !!this.value)
			{
				this.value();

				window.setTimeout(() => { (event.target as HTMLElement).focus(); }, 10); // 10ms for `focus-trap` attributes

				return false;
			}
		}

		return true;
	};

	public attached()
	{
		this.taskQueue.queueTask(() =>
		{
			PLATFORM.addEventListener("click", this.listener, false);
		});
	}

	public detached()
	{
		this.taskQueue.queueTask(() =>
		{
			PLATFORM.removeEventListener("click", this.listener, false);
		});
	}
}
