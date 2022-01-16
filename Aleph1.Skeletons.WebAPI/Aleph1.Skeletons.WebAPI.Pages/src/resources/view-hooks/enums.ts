import { ViewEngineHooks, View } from "aurelia-framework";
import { Roles } from "resources/enums";

export class EnumsViewEngineHooks implements ViewEngineHooks
{
	// eslint-disable-next-line class-methods-use-this
	public beforeBind(view: View & { overrideContext: any }): void
	{
		view.overrideContext.Roles = Roles;
	}
}
