import { autoinject } from "aurelia-framework";
import { PersonService } from "./person-service";
import { PersonModel } from "./person-model";

@autoinject()
export class Persons
{
	constructor(
		private personService: PersonService
	) { }

	public persons: PersonModel[];

	public async activate(): Promise<void>
	{
		this.persons = await this.personService.getPersons();
	}
}
