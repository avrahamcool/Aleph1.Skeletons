import { Roles } from "resources/models/roles";
import { ViewEngineHooks, View } from "aurelia-framework";

export class EnumsViewEngineHooks implements ViewEngineHooks
{
	beforeBind(view: View): void
	{
		view.overrideContext["Roles"] = Roles;
	}
}
