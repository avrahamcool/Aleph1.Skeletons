import { PLATFORM } from "aurelia-pal";
import { FrameworkConfiguration } from "aurelia-framework";

export function configure(config: FrameworkConfiguration): void
{
	config.globalResources([
		PLATFORM.moduleName("./attributes/click-outside"),
		PLATFORM.moduleName("./attributes/enter-press"),
		PLATFORM.moduleName("./attributes/flatpickr"),
		PLATFORM.moduleName("./attributes/focus-trap"),
		PLATFORM.moduleName("./attributes/mask"),
		PLATFORM.moduleName("./attributes/overlay-scrollbar"),
		PLATFORM.moduleName("./attributes/tippy"),
		PLATFORM.moduleName("./binding-behaviours/async"),
		PLATFORM.moduleName("./binding-behaviours/debounce-enter"),
		PLATFORM.moduleName("./value-converters/allowed-routes"),
		PLATFORM.moduleName("./value-converters/array-length"),
		PLATFORM.moduleName("./value-converters/date-format"),
		PLATFORM.moduleName("./value-converters/json"),
		PLATFORM.moduleName("./value-converters/number-with-commas"),
		PLATFORM.moduleName("./value-converters/pad-number"),
		PLATFORM.moduleName("./value-converters/purify-html"),
		PLATFORM.moduleName("./view-hooks/enums")
	]);
}
