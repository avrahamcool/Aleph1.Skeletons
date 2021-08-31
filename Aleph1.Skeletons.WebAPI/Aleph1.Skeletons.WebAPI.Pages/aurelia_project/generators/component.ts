import { inject } from "aurelia-dependency-injection";
import { Project, ProjectItem, CLIOptions, UI } from "aurelia-cli";
import * as path from "path";

@inject(Project, CLIOptions, UI)
export default class ElementGenerator
{
	constructor(private project: Project, private options: CLIOptions, private ui: UI) { }

	async execute(): Promise<void>
	{
		const name = await this.ui.ensureAnswer(
			this.options.args[0],
			"What would you like to call the component?"
		);

		const subFolders = await this.ui.ensureAnswer(
			this.options.args[1],
			'What sub-folder would you like to add it to?\nIf it doesn\'t exist it will be created for you.\n\nDefault folder is "." relative to the source folder src/', "."
		);

		const fileName = this.project.makeFileName(name);
		const className = this.project.makeClassName(name);

		this.project.root.add(
			ProjectItem.text(path.join(subFolders, fileName + ".ts"), this.generateJSSource(className)),
			ProjectItem.text(path.join(subFolders, fileName + ".html"), this.generateHTMLSource(className))
		);

		await this.project.commitChanges();
		await this.ui.log(`Created ${ name } in the '${ path.join(this.project.root.name, subFolders) }' folder`);
	}

	generateJSSource(className: string): string
	{
		return `export class ${ className }
{
	message: string;

	constructor()
	{
    	this.message = 'Hello world';
	}
}
`;
	}

	generateHTMLSource(className: string): string
	{
		return `<template>
	<h1>${ className } message: \${message}</h1>
</template>
`;
	}
}
