import { ComposeCellRenderer } from "resources/elements/compose-cell-renderer";
import { ColDef, Grid, GridOptions } from "ag-grid-community";
import { Cell } from "./cell";

export class Persons
{
	columnDefs: ColDef[] = [
		{ headerName: "Make", field: "make" },
		{ headerName: "Model", field: "model" },
		{ headerName: "Price", field: "price" },
		{
			cellRenderer: ComposeCellRenderer,
			cellRendererParams:
			{
				viewModel: Cell
			}
		}
	];

	rowData = [
		{ make: "Toyota", model: "Celica", price: 35000 },
		{ make: "Ford", model: "Mondeo", price: 32000 },
		{ make: "Porsche", model: "Boxter", price: 72000 }
	];

	table: HTMLDivElement;
	grid: Grid;
	private attached()
	{
		const gridOptions: GridOptions = {
			columnDefs: this.columnDefs,
			rowData: this.rowData
		};

		this.grid = new Grid(this.table, gridOptions);
	}
	private detached()
	{
		this.grid.destroy();
	}

}
