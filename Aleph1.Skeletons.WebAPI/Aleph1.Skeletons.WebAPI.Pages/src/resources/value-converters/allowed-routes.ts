import { autoinject } from "aurelia-framework";
import { NavModel } from "aurelia-router";
import { UserService } from "resources/services";

@autoinject()
export class AllowedRoutesValueConverter
{
	constructor(
		private userService: UserService
	) { }

	public toView(routes: NavModel[]): NavModel[]
	{
		return routes.filter(r => this.userService.isAllowedForRole(r.config.settings.auth));
	}
}
