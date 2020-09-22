export class PersonModel
{
	constructor(fromServer?: PersonModel)
	{
		if (fromServer)
		{
			Object.assign(this, fromServer);
			this.birthDate = fromServer.birthDate && new Date(fromServer.birthDate);
		}
	}

	/** auto increment (1, 1) */
	public id: number;

	/** first name - 256 max length */
	public firstName: string;

	/** last name - 256 max length */
	public lastName: string;

	/** birth date */
	public birthDate: Date;
}
