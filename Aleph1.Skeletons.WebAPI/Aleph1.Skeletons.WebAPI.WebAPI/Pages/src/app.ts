import { autoinject } from "aurelia-framework";
import { UserService } from "resources";
import { PLATFORM } from "aurelia-pal";
import { RouterConfiguration, NavigationInstruction, Next, Redirect } from "aurelia-router";
import { Roles } from "resources";

@autoinject
export class App
{
	constructor(private userService: UserService) { }
	configureRouter(config: RouterConfiguration): void
	{
		config.title = "Demo App";
		config.map([
			{
				route: "login",
				name: "login",
				moduleId: PLATFORM.moduleName("./components/login/login"),
				title: "login"
			},
			{
				route: ["", "persons"],
				name: "persons",
				moduleId: PLATFORM.moduleName("./components/persons/persons"),
				title: "persons",
				nav: true,
				settings: {
					auth: Roles.User
				}
			},
			{
				route: "admin",
				name: "admin",
				moduleId: PLATFORM.moduleName("./components/admin/admin"),
				title: "admin",
				nav: true,
				settings: {
					auth: Roles.Admin
				}
			}
		]);
		config.addAuthorizeStep({
			run: (instruction: NavigationInstruction, next: Next) =>
			{
				const requiredRoles = instruction
					.getAllInstructions()
					.map(i => i.config.settings?.auth as Roles || Roles.Anonymous);

				if (!requiredRoles.every(r => this.userService.isAllowedForRole(r)))
				{
					this.userService.redirectAfterLogin = instruction.fragment + (instruction.queryString ? `?${ instruction.queryString }` : "");
					return next.cancel(new Redirect("login"));
				}

				return next();
			}
		})
	}
}
