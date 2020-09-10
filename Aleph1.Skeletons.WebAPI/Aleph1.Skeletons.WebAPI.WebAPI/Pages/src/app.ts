import { autoinject } from "aurelia-framework";
import { UserService } from "resources";
import { PLATFORM } from "aurelia-pal";
import { RouterConfiguration, NavigationInstruction, Next, Redirect } from "aurelia-router";

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
					auth: true
				}
			},
			{
				route: "test",
				name: "test",
				moduleId: PLATFORM.moduleName("./components/test/test.html"),
				title: "test",
				nav: true,
				settings: {
					auth: true
				}
			}
		]);
		config.addAuthorizeStep({
			run: (instruction: NavigationInstruction, next: Next) =>
			{
				if (instruction.getAllInstructions().some(i => i.config.settings.auth))
				{
					if (!this.userService.isLoggedIn)
					{
						return next.cancel(new Redirect("login"));
					}
				}

				return next();
			}
		})
	}
}
