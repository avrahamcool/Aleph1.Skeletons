import { autoinject, Aurelia } from "aurelia-framework";
import { Roles } from "resources/models";
import { UserService } from "resources/services";
import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration, NavigationInstruction, Next } from "aurelia-router";


@autoinject
export class App
{
	constructor(private au: Aurelia, private userService: UserService, private router: Router)
	{ }

	public activate(): void
	{
		this.router.configure((config: RouterConfiguration) =>
		{
			config.title = "Demo App";
			config.map([
				{
					route: ["", "persons"],
					name: "persons",
					moduleId: PLATFORM.moduleName("../components/persons/persons"),
					title: "persons",
					nav: true,
					settings: {
						auth: Roles.User
					}
				},
				{
					route: "admin",
					name: "admin",
					moduleId: PLATFORM.moduleName("../components/admin/admin"),
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
						this.au.setRoot(PLATFORM.moduleName("shells/login"));
						return next.cancel();
					}

					return next();
				}
			});

			return config;
		})

	}
}
