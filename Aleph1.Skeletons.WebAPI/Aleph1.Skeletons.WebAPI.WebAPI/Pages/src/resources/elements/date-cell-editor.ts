import { IAfterGuiAttachedParams, ICellEditorComp, ICellEditorParams, Promise } from "ag-grid-community";
import flatpickr from "flatpickr";

export class DateCellEditor implements ICellEditorComp
{
	private inputEl: HTMLInputElement;
	private instance: flatpickr.Instance;
	init?(params: ICellEditorParams): void | Promise<void>
	{
		this.inputEl = document.createElement("input");
		this.inputEl.style.width = "100%";
		this.instance = flatpickr(this.inputEl, {
			allowInput: true,
			defaultDate: params.value,
			dateFormat: "d/m/Y"
		});
	}
	getGui(): HTMLElement
	{
		return this.inputEl;
	}
	focusIn?(): void
	{
		this.inputEl.focus();
	}
	getValue()
	{
		return this.instance.selectedDates[0];
	}
	focusOut?(): void
	{
		//
	}
	isCancelAfterEnd?(): boolean
	{
		return false;
	}
	destroy?(): void
	{
		this.instance.destroy();
	}
}
