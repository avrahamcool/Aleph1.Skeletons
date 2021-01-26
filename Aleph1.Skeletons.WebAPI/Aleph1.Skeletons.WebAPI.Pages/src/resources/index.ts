import { PLATFORM } from "aurelia-pal";
import { FrameworkConfiguration } from "aurelia-framework";

export function configure(config: FrameworkConfiguration): void
{
	config.globalResources([
		PLATFORM.moduleName("./binding-behaviors/async"),
		PLATFORM.moduleName("./value-converters/allowed-routes"),
		PLATFORM.moduleName("./value-converters/date-format"),
		PLATFORM.moduleName("./value-converters/json"),
		PLATFORM.moduleName("./view-hooks/enums")
	]);
}
