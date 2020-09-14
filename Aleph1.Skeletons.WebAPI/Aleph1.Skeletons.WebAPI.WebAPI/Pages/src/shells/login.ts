import { UserService } from "resources/services";
import { autoinject } from "aurelia-framework";
import { LoginModel } from "resources/models";

@autoinject
export class LoginSell
{
	credentials: LoginModel = new LoginModel();
	constructor(public userService: UserService) { }
}
