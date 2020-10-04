import { PLATFORM } from "aurelia-pal";
import { ComposeCellRenderer } from "./../../resources/elements/compose-cell-renderer";
import { SelectedRowsStatus } from "resources/elements/selected-rows-status";
import { ComposeStatusPanel } from "resources/elements/compose-status-panel";
import { DateCellEditor } from "resources/elements/date-cell-editor";
import { autoinject } from "aurelia-framework";
import { PersonService } from "./person-service";
import { ColDef, ColGroupDef, Grid, GridOptions, RowValueChangedEvent, ValueGetterParams, ValueFormatterParams, FirstDataRenderedEvent, RowDataUpdatedEvent } from "ag-grid-community";
import { PersonModel } from "./person-model";
import format from "date-fns/format";
import differenceInYears from "date-fns/differenceInYears";
import { nameof } from "ts-simple-nameof";

@autoinject
export class Persons
{
	constructor(private personService: PersonService) { }

	persons: PersonModel[];
	personsEl: HTMLDivElement;
	personsColumns: (ColDef | ColGroupDef)[] = [
		{
			headerName: "ID",
			field: nameof<PersonModel>(p => p.id),
			pinned: "left",
			filter: "agNumberColumnFilter",
			checkboxSelection: true,
			headerCheckboxSelection: true,
			headerCheckboxSelectionFilteredOnly: true,
			maxWidth: 100
		},
		{
			headerName: "Name",
			children: [
				{
					headerName: "First",
					field: nameof<PersonModel>(p => p.firstName),
					editable: true,
					filter: "agTextColumnFilter",
					cellRenderer: "agAnimateShowChangeCellRenderer"
				},
				{
					headerName: "Last",
					field: nameof<PersonModel>(p => p.lastName),
					editable: true,
					filter: "agTextColumnFilter",
					cellRenderer: "agAnimateShowChangeCellRenderer"
				},
				{
					headerName: "Full",
					valueGetter: (vg: ValueGetterParams) => `${ vg.data.lastName } ${ vg.data.firstName }`,
					flex: 1,
					cellRenderer: "agAnimateShowChangeCellRenderer"
				}
			]
		},
		{
			headerName: "Gender",
			field: nameof<PersonModel>(p => p.gender),
			cellRenderer: ComposeCellRenderer,
			cellRendererParams: { view: PLATFORM.moduleName("components/persons/cells/gender-cell.html") }
		},
		{
			headerName: "Date",
			children: [
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
					valueGetter: (vg: ValueGetterParams) => differenceInYears(new Date(), vg.data.birthDate),
					cellRenderer: "agAnimateShowChangeCellRenderer"
				}]
		}
	];
	gridOptions: GridOptions = {
		defaultColDef: {
			sortable: true,
			filterParams: {
				buttons: ["reset", "apply"],
				debounceMs: 200
			}
		},
		columnDefs: this.personsColumns,
		suppressCellSelection: true,
		editType: "fullRow",
		rowSelection: "multiple",
		suppressRowClickSelection: true,
		//stopEditingWhenGridLosesFocus: true,
		//getRowNodeId: (p: PersonModel) => p.id.toString(),
		onRowValueChanged: (event: RowValueChangedEvent) =>
		{
			this.personService.updatePerson(event.data);
		},
		onFirstDataRendered: (event: FirstDataRenderedEvent) =>
		{
			event.api.sizeColumnsToFit();
		},
		onRowDataUpdated: (event: RowDataUpdatedEvent) =>
		{
			//event.
		},
		statusBar: {
			statusPanels: [
				{
					statusPanel: ComposeStatusPanel,
					statusPanelParams: {
						viewModel: SelectedRowsStatus
					},
					align: "left"
				},
				{
					statusPanel: "agTotalRowCountComponent",
					align: "right"
				}
			]
		}
	};
	personsGrid: Grid;

	public activate(): Promise<PersonModel[]>
	{
		return this.personService.getPersons()
			.then(persons => this.persons = persons);
	}
	public attached(): void
	{
		this.personsGrid = new Grid(this.personsEl, this.gridOptions);
		this.gridOptions.api.setRowData(this.persons);
	}
	public detached(): void
	{
		this.personsGrid.destroy();
	}
}
