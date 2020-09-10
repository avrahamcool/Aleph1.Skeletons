import { PLATFORM } from "aurelia-pal";
import { RouterConfiguration } from "aurelia-router";

export class App
{
	configureRouter(config: RouterConfiguration): void
	{
		config.title = "Demo App";
		config.map([
			{
				route: ["", "login"],
				name: "login",
				moduleId: PLATFORM.moduleName("./components/login/login"),
				title: "login"
			},
			{
				route: "persons",
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
	}
}
