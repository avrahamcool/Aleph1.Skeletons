import { autoinject } from "aurelia-framework";
import { Router } from "aurelia-router";
import { UserService } from "resources/services";
import { busyTracking, handleErrors } from "resources/decorators";

@autoinject()
export class AppHeader
{
	constructor(
		public router: Router,
		private userService: UserService
	) { }

	@handleErrors("התרחשה שגיאה בעת ההתנתקות")
	@busyTracking("signOutPending")
	public async signOutHandler(): Promise<void>
	{
		await this.userService.signOut();
	}
}
