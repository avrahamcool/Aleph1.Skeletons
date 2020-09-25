import { PLATFORM } from "aurelia-pal";
import { FrameworkConfiguration } from "aurelia-framework";

export function configure(config: FrameworkConfiguration): void
{
	config.globalResources([
		PLATFORM.moduleName("./view-hooks/enums"),
		PLATFORM.moduleName("./value-converters/json"),
		PLATFORM.moduleName("./value-converters/date-format"),
		PLATFORM.moduleName("./value-converters/allowed-routes")
	]);
}
