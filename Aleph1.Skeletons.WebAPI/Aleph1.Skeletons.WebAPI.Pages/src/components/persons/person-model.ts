import { Type } from "class-transformer";

export class PersonModel
{
	public id: number;

	public firstName: string;

	public lastName: string;

	@Type(() => Date)
	public birthdate: Date;
}
