import "reflect-metadata";
import { Aurelia } from "aurelia-framework";
import { PLATFORM } from "aurelia-pal";
import { DialogConfiguration } from "aurelia-dialog";
import { Notyf } from "notyf";
import { UserService } from "resources/services";
import { default as environment } from "../config/environment.json";

import "./main.scss";

export function configure(aurelia: Aurelia): void
{
	aurelia.use
		.standardConfiguration()
		.feature(PLATFORM.moduleName("resources/index"))
		.plugin(PLATFORM.moduleName("aurelia-dialog"), (config: DialogConfiguration) =>
		{
			config.useDefaults();
			config.useCSS("");
			config.settings.ignoreTransitions = true;
			config.settings.keyboard = "Escape";
		})
		.plugin(PLATFORM.moduleName("aurelia-deep-computed"));

	aurelia.use.developmentLogging(environment.debug ? "debug" : "warn");

	if (environment.testing)
	{
		aurelia.use.plugin(PLATFORM.moduleName("aurelia-testing"));
	}

	aurelia.container.registerInstance(
		Notyf,
		new Notyf({
			duration: 5000,
			position: { x: "left", y: "bottom" },
			dismissible: true
		})
	);

	aurelia.start().then(() =>
	{
		const userService = aurelia.container.get(UserService);

		if (userService.isSignedIn)
		{
			aurelia.setRoot(PLATFORM.moduleName("shells/app"))
				.then(() => userService.startIdleTimeout());
		}
		else
		{
			aurelia.setRoot(PLATFORM.moduleName("shells/sign-in"));
		}
	});
}
