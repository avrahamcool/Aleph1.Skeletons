import { autoinject } from "aurelia-framework";
import { UserService } from "resources/services";
import { NavModel } from "aurelia-Router";

@autoinject
export class AllowedRoutesValueConverter
{
	constructor(private userService: UserService) { }

	toView(routes: NavModel[]): NavModel[]
	{
		return routes.filter(r => this.userService.isAllowedForRole(r.config.settings.auth));
	}
}
