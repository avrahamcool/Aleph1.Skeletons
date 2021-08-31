import { autoinject, bindable, bindingMode } from "aurelia-framework";
import { default as flatpickr } from "flatpickr";
import { Instance } from "flatpickr/dist/types/instance";
import { Hebrew } from "flatpickr/dist/l10n/he";
import { Options } from "flatpickr/dist/types/options";

@autoinject()
export class FlatpickrCustomAttribute
{
	constructor(element: Element)
	{
		if (!(element instanceof HTMLInputElement))
		{
			throw new Error("Custom attribute `flatpickr` can only be used on input elements!");
		}
		this.element = element;
	}

	@bindable({ defaultBindingMode: bindingMode.twoWay })
	private dateValue: Date;

	@bindable({ defaultBindingMode: bindingMode.twoWay })
	private dateValues: Date[];

	@bindable({ defaultBindingMode: bindingMode.oneTime })
	private options: Partial<Options>;

	@bindable({ defaultBindingMode: bindingMode.toView })
	private mode: "single" | "range" = "single";

	private element: HTMLInputElement;
	private instance: Instance;

	public attached()
	{
		const options = this.options || {};

		this.instance = flatpickr(this.element,
			{
				locale: Hebrew,
				dateFormat: "d/m/Y",
				allowInput: true,
				time_24hr: true,
				enableTime: false,
				...options,
				onChange: selectedDates =>
				{
					const selectedDate = selectedDates?.[0];
					if (selectedDate?.getTime() !== this.dateValue?.getTime())
					{
						this.dateValue = selectedDate;
						// selectedDate && instance.close();
					}
					this.dateValues = selectedDates;
				},
				mode: this.mode
			});

		this.instance.setDate(this.dateValue || this.dateValues);
	}

	public detached()
	{
		this.instance.destroy();
	}

	public dateValueChanged(newValue: Date): void
	{
		this.instance?.setDate(newValue);
	}

	public dateValuesChanged(newValue: Date[]): void
	{
		this.instance?.setDate(newValue);
	}

	public modeChanged()
	{
		if (this.instance)
		{
			this.instance.set("mode", this.mode);
			// removing the second date in case we return to 'single'. also safe for other cases.
			this.instance.setDate(this.instance.selectedDates[0], true);
		}
	}
}
