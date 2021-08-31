import { autoinject, Aurelia } from "aurelia-framework";
import { PLATFORM } from "aurelia-pal";
import { Router, RouterConfiguration, NavigationInstruction, Next } from "aurelia-router";
import { Roles } from "resources/enums";
import { UserService } from "resources/services";

@autoinject()
export class AppShell
{
	constructor(
		private aurelia: Aurelia,
		private userService: UserService,
		private router: Router
	) { }

	public activate(): void
	{
		this.router.configure((config: RouterConfiguration) =>
		{
			config.title = "Demo App";
			config.fallbackRoute("persons");

			config.map([
				{
					route: ["", "persons"],
					name: "persons",
					moduleId: PLATFORM.moduleName("components/persons/persons"),
					title: "Persons",
					nav: true,
					settings: {
						auth: Roles.User
					}
				},
				{
					route: "admin",
					name: "admin",
					moduleId: PLATFORM.moduleName("components/admin/admin"),
					title: "Admin",
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
						.map(i => i.config.settings?.auth as Roles || Roles.None);

					if (!requiredRoles.every(r => this.userService.isAllowedForRole(r)))
					{
						this.aurelia.setRoot(PLATFORM.moduleName("shells/sign-in"));
						return next.cancel();
					}

					return next();
				}
			});

			return config;
		});
	}
}
