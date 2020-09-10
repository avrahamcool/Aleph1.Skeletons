import { UserService } from "resources";
import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";
import { LoginService } from "components/login/login-service";

@autoinject
export class AppHeader
{
	constructor(public router: Router, public loginService: LoginService, public userService: UserService)
	{ }
}
