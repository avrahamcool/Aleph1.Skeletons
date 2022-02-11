import { Roles } from "resources/models/roles";
import { ViewEngineHooks, View } from "aurelia-framework";

export class EnumsViewEngineHooks implements ViewEngineHooks
{
	beforeBind(view: View): void
	{
		Object.assign(view.overrideContext, { Roles } );
	}
}
