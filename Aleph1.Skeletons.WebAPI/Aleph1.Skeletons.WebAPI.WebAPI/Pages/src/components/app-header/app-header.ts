import { UserService } from "resources/services";
import { Router } from "aurelia-router";
import { autoinject } from "aurelia-framework";

@autoinject
export class AppHeader
{
	constructor(public router: Router, public userService: UserService)
	{ }
}
