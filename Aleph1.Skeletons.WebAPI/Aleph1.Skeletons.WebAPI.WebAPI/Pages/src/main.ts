import { UserService } from "resources/services";
import { Aurelia } from "aurelia-framework";
import { PLATFORM } from "aurelia-pal";
import * as environment from "../config/environment.json";

import "./main.scss";

export function configure(aurelia: Aurelia): void
{
	aurelia.use
		.standardConfiguration()
		.feature(PLATFORM.moduleName("resources/index"))
		.plugin(PLATFORM.moduleName("aurelia-dialog"));

	aurelia.use.developmentLogging(environment.debug ? "debug" : "warn");

	if (environment.testing)
	{
		aurelia.use.plugin(PLATFORM.moduleName("aurelia-testing"));
	}

	aurelia.start().then(() =>
	{
		const userService = aurelia.container.get(UserService);
		if (userService.isLoggedIn)
		{
			aurelia.setRoot(PLATFORM.moduleName("shells/app"))
				.then(() => userService.startIdleTimeout());
		}
		else
		{
			aurelia.setRoot(PLATFORM.moduleName("shells/login"));
		}
	});
}
