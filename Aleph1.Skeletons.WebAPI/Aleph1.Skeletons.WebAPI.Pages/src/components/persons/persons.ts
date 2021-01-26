import { autoinject } from "aurelia-framework";
import { PersonService } from "./person-service";
import { PersonModel } from "./person-model";

@autoinject()
export class Persons
{
	constructor(private personService: PersonService) { }
	persons: PersonModel[];

	public activate(): Promise<PersonModel[]>
	{
		return this.personService.getPersons()
			.then(persons => this.persons = persons);
	}
}
