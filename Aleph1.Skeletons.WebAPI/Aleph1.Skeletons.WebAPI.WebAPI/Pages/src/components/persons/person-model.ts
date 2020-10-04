import { Type } from "class-transformer";

export class PersonModel
{
	/** auto increment (1, 1) */
	public id: number;

	/** first name - 256 max length */
	public firstName: string;

	/** last name - 256 max length */
	public lastName: string;

	/** birth date */
	@Type(() => Date)
	public birthDate: Date;
}
