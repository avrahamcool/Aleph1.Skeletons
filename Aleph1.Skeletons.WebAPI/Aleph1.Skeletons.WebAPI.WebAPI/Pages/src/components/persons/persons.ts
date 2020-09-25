import { DateCellEditor } from "resources/elements/date-cell-editor";
import { autoinject } from "aurelia-framework";
import { PersonService } from "./person-service";
import { ComposeCellRenderer } from "resources/elements/compose-cell-renderer";
import { ColDef, Grid, GridOptions, RowValueChangedEvent, ValueGetterParams, ValueFormatterParams } from "ag-grid-community";
import { Cell } from "./cell";
import { PersonModel } from "./person-model";
import format from "date-fns/format";
import differenceInYears from "date-fns/differenceInYears";

@autoinject
export class Persons
{
	constructor(private personService: PersonService) { }

	persons: PersonModel[];
	personsEl: HTMLDivElement;
	personsColumns: ColDef[] = [
		{ headerName: "ID", field: "id", pinned: "left", filter: "agNumberColumnFilter" },
		{ headerName: "First", field: "firstName", flex: 1, editable: true, filter: "agTextColumnFilter" },
		{ headerName: "Last", field: "lastName", editable: true, filter: "agTextColumnFilter" },
		{
			headerName: "Birth Date",
			field: "birthDate",
			editable: true,
			filter: "agDateColumnFilter",
			cellRenderer: "agAnimateShowChangeCellRenderer",
			cellEditor: DateCellEditor,
			valueFormatter: (vf: ValueFormatterParams) => format(vf.value as Date, "dd/MM/yyyy")
		},
		{
			headerName: "Age",
			valueGetter: (vg: ValueGetterParams) => differenceInYears(new Date(), vg.data.birthDate)
		},
		{
			cellRenderer: ComposeCellRenderer,
			cellRendererParams: { viewModel: Cell }
		}
	];
	gridOptions: GridOptions;
	personsGrid: Grid;

	public activate(): Promise<PersonModel[]>
	{
		return this.personService.getPersons()
			.then(persons => this.persons = persons);
	}
	public attached(): void
	{
		this.gridOptions = {
			defaultColDef: {
				resizable: true,
				filterParams: {
					buttons: ["reset", "apply"],
					debounceMs: 200
				}
			},
			columnDefs: this.personsColumns,
			rowData: this.persons,
			suppressCellSelection: true,
			editType: "fullRow",
			//stopEditingWhenGridLosesFocus: true,
			//getRowNodeId: (p: PersonModel) => p.id.toString(),
			onRowValueChanged: (event: RowValueChangedEvent) =>
			{
				// call API with update logic
			}
		};

		this.personsGrid = new Grid(this.personsEl, this.gridOptions);
	}
	public detached(): void
	{
		this.personsGrid.destroy();
	}
}
