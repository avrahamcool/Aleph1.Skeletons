import { LoginService } from "./login-service";
import { autoinject } from "aurelia-framework";
import { LoginModel } from "./login-model";

@autoinject
export class Login
{
	credentials: LoginModel = new LoginModel();
	constructor(public loginService: LoginService) { }
}
