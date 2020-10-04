import { IStatusPanelComp, IStatusPanelParams } from "ag-grid-community";
import { TemplatingEngine, View, Container } from "aurelia-framework";

export class ComposeStatusPanel implements IStatusPanelComp
{
	private templatingEngine: TemplatingEngine;
	private params: IStatusPanelParams;
	private enhancedView: View;

	init?(params: IStatusPanelParams): void
	{
		this.templatingEngine = Container.instance.get(TemplatingEngine);
		this.params = params;
	}

	getGui(): HTMLElement
	{
		const compose = document.createElement("compose");
		compose.setAttribute("view-model.bind", "viewModel");
		compose.setAttribute("view.bind", "view");
		compose.setAttribute("model.bind", "$this");
		compose.classList.add("block", "h-full", "py-2");

		// element = <compose view-model.bind="viewModel" view.bind="view" model.bind="model"></compose>
		this.enhancedView = this.templatingEngine.enhance({ element: compose, bindingContext: this.params });
		return compose;
	}

	destroy?(): void
	{
		this.enhancedView.detached();
		this.enhancedView.unbind();
	}
}
