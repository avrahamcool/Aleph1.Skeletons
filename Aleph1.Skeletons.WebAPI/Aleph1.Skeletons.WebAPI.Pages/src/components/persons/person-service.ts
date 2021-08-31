import { autoinject } from "aurelia-framework";
import { PersonModel } from "./person-model";
import { AuthHttpClient } from "resources/services/auth-http-client";
import { plainToClass } from "class-transformer";

@autoinject()
export class PersonService
{
	constructor(
		private httpClient: AuthHttpClient
	) { }

	public async getPersons(): Promise<PersonModel[]>
	{
		const response = await this.httpClient.get("/api/persons");
		const data: object[] = await response.json();
		return plainToClass(PersonModel, data);
	}
}
