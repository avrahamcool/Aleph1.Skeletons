import { autoinject } from "aurelia-framework";
import { UserService } from "resources/services";
import { displayCustomError } from "resources/helpers";
import { busyTracking } from "resources/decorators";

@autoinject()
export class AppHeader
{
	constructor(
		private userService: UserService
	) { }

	@busyTracking("signOutPending")
	public async signOutHandler(): Promise<void>
	{
		try
		{
			await this.userService.signOut();
		}
		catch
		{
			displayCustomError("התרחשה שגיאה בעת ההתנתקות");
		}
	}
}
