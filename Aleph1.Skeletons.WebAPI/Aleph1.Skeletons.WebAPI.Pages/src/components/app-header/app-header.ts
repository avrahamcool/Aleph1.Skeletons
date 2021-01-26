import { UserService } from "resources/services";
import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { handleErrors } from "resources/decorators/handle-errors";

@autoinject()
export class AppHeader
{
	constructor(public router: Router, public userService: UserService)
	{ }

	@handleErrors("התרחשה שגיאה בעת ההתנתקות")
	public logout(): Promise<void>
	{
		return this.userService.logout();
	}
}
