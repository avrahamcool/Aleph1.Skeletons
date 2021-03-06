import { inject } from "aurelia-dependency-injection";
import { Project, ProjectItem, CLIOptions, UI } from "aurelia-cli";

@inject(Project, CLIOptions, UI)
export default class ElementGenerator
{
	constructor(private project: Project, private options: CLIOptions, private ui: UI) { }

	async execute(): Promise<void>
	{
		const name = await this.ui.ensureAnswer(
			this.options.args[0],
			"What would you like to call the custom element?"
		);

		const fileName = this.project.makeFileName(name);
		const className = this.project.makeClassName(name);

		this.project.elements.add(
			ProjectItem.text(`${ fileName }.ts`, this.generateJSSource(className)),
			ProjectItem.text(`${ fileName }.html`, this.generateHTMLSource(className))
		);

		await this.project.commitChanges();
		await this.ui.log(`Created ${ fileName }.`);
	}

	generateJSSource(className: string): string
	{
		return `import {bindable} from "aurelia-framework";

export class ${ className }
{
	@bindable value;

	valueChanged(newValue, oldValue)\
	{
	}
}
`;
	}

	generateHTMLSource(className: string): string
	{
		return `<template>
	<h1>${ className } value: \${value}</h1>
</template>
`;
	}
}
