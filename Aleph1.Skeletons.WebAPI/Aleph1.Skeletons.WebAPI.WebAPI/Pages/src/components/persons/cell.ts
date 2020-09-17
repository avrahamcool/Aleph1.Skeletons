import { ICellRendererParams } from "ag-grid-community";

export class Cell
{
	currentRow: any;
	activate(params: ICellRendererParams)
	{
		this.currentRow = params.data;
	}
	doStuff()
	{
		console.log(this.currentRow);
	}
}
