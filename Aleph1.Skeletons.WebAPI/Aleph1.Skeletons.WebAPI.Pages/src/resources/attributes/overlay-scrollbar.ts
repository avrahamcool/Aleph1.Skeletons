import { autoinject, bindable, bindingMode } from "aurelia-framework";
import { default as OverlayScrollbars } from "overlayscrollbars";

@autoinject()
export class OverlayScrollbarCustomAttribute
{
	constructor(element: Element)
	{
		if (!(element instanceof HTMLElement))
		{
			throw new Error("Custom attribute `overlay-scrollbar` can only be used on HTML elements!");
		}
		this.element = element;
	}

	@bindable({ defaultBindingMode: bindingMode.oneTime, primaryProperty: true })
	private options: Partial<OverlayScrollbars.Options>;

	private element: HTMLElement;
	private instance: OverlayScrollbars;

	public attached()
	{
		this.instance = OverlayScrollbars(this.element, {
			overflowBehavior: {
				x: "hidden",
				y: "scroll"
			},
			scrollbars: {
				visibility: "auto",
				autoHide: "leave",
				autoHideDelay: 1000,
				dragScrolling: true,
				clickScrolling: true,
				touchSupport: true,
				snapHandle: false
			},
			...this.options
		});
	}

	public detached()
	{
		this.instance.destroy();
	}
}
