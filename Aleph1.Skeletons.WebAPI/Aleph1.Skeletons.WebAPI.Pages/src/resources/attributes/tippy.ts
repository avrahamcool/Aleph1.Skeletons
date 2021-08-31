import { autoinject, bindable, bindingMode } from "aurelia-framework";
import { default as tippy, Instance, Props, Content } from "tippy.js";

@autoinject()
export class TippyCustomAttribute
{
	constructor(element: Element)
	{
		if (!(element instanceof HTMLElement))
		{
			throw new Error("Custom attribute `tippy` can only be used on HTML elements!");
		}
		this.element = element;
	}

	@bindable({ defaultBindingMode: bindingMode.toView, primaryProperty: true })
	private options: Content | Props;

	private element: HTMLElement;
	private instance: Instance<Props>;

	public attached()
	{
		const options = TippyCustomAttribute.isContent(this.options)
			? { content: this.options } : this.options;

		this.instance = tippy(
			this.element,
			{
				hideOnClick: false,
				delay: 300,
				...options
			}
		);
	}

	public detached()
	{
		this.instance.destroy();
	}

	public optionsChanged(newValue: Content | Props)
	{
		if (!this.instance || this.instance.state.isDestroyed)
		{
			return;
		}

		if (TippyCustomAttribute.isContent(newValue))
		{
			this.instance.setContent(newValue);
		}
		else
		{
			this.instance.setProps(newValue);
		}
	}

	private static isContent(possibleContent: Content | Props): possibleContent is Content
	{
		return ["string", "function"].includes(typeof possibleContent) || this.isElement(possibleContent);
	}

	private static isElement(value: unknown): value is Element | DocumentFragment
	{
		return ["Element", "Fragment"].some(type => this.isType(value, type));
	}

	private static isType(value: unknown, type: string): boolean
	{
		const str = {}.toString.call(value);
		return str.indexOf("[object") === 0 && str.indexOf(`${type}]`) > -1;
	}
}
