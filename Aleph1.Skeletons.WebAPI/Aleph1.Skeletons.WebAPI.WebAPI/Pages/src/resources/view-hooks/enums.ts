import { Roles } from "./../models/roles";

import { ViewEngineHooks, View } from 'aurelia-framework';

export class EnumsViewEngineHooks implements ViewEngineHooks
{
	beforeBind(view: View)
	{
		view.overrideContext['Roles'] = Roles;
	}
}
