import { autoinject } from "aurelia-framework";
import { PersonModel } from "./person-model";
import { AuthHttpClient } from "resources/services/auth-http-client";
import { plainToClass } from "class-transformer";

@autoinject()
export class PersonService
{
	constructor(private httpClient: AuthHttpClient)
	{ }

	public getPersons(): Promise<PersonModel[]>
	{
		return this.httpClient.get("/api/persons")
			.then(resp => resp.json())
			.then((fromServer: PersonModel[]) => plainToClass(PersonModel, fromServer));
	}
}
