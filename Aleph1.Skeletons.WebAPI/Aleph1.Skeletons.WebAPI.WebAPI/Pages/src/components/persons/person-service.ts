import { autoinject } from "aurelia-framework";
import { PersonModel } from "./person-model";
import { AuthHttpClient } from "resources/services/auth-http-client";

@autoinject
export class PersonService
{
	constructor(private httpClient: AuthHttpClient)
	{ }

	public getPersons(): Promise<PersonModel[]>
	{
		return this.httpClient.get("/api/Persons")
			.then(resp => resp.json())
			.then((fromServer: PersonModel[]) => fromServer.map(p => new PersonModel(p)));
	}
}
