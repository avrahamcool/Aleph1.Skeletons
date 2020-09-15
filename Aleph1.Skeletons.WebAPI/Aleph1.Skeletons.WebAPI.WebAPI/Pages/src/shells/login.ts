import { UserService } from "resources/services";
import { autoinject } from "aurelia-framework";
import { LoginModel } from "resources/models";
import { handleErrors } from "resources/decorators/handle-errors";

@autoinject
export class LoginSell
{
	credentials: LoginModel = new LoginModel();
	constructor(public userService: UserService) { }

	@handleErrors("התרחשה שגיאה בעת ההתחברות")
	public login(): Promise<void>
	{
		return this.userService.login(this.credentials);
	}
}
