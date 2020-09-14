import { Aurelia } from "aurelia-framework";
import { PLATFORM } from "aurelia-pal";

import * as environment from "../config/environment.json";

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

	aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName("app")));
}
