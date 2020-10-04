import { Roles } from "./../models/roles";
import { Genders } from "components/persons/gender";
import { ViewEngineHooks, View } from "aurelia-framework";

export class EnumsViewEngineHooks implements ViewEngineHooks
{
	beforeBind(view: View): void
	{
		view.overrideContext["Roles"] = Roles;
		view.overrideContext["Genders"] = Genders;
	}
}
