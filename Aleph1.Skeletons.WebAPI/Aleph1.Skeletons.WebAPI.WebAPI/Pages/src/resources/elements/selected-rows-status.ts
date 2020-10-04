import { IStatusPanelParams } from "ag-grid-community";

export class SelectedRowsStatus
{
	private params: IStatusPanelParams;

	public selected: unknown[] = [];
	private updateSelected = () => this.selected = this.params.api.getSelectedRows();

	public deleteSelected(): void
	{
		this.params.api.applyTransaction({
			remove: this.selected
		});
		this.selected = [];
	}
	public activate(params: IStatusPanelParams): void
	{
		this.params = params;
		this.params.api.addEventListener("selectionChanged", this.updateSelected);
	}
	public detached(): void
	{
		this.params.api.removeEventListener("selectionChanged", this.updateSelected);
	}
}
