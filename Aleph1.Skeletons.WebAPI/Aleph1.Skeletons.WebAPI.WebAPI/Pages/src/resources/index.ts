import { PLATFORM } from "aurelia-pal";
import { FrameworkConfiguration } from "aurelia-framework";

export function configure(config: FrameworkConfiguration): void
{
	config.globalResources([
		PLATFORM.moduleName("./view-hooks/enums"),
		PLATFORM.moduleName("./value-converters/json"),
		PLATFORM.moduleName("./value-converters/allowed-routes")
	]);
}

export * from "./models/roles";
export * from "./models/authentication-info";

export * from "./services/user-service";
export * from "./services/auth-http-client";
