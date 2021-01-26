export enum Roles
{
	/** Not logged in */
	Anonymous = 0b000,

	/** Regular user */
	User = 0b001,

	//AnotherRole = 0b010,
	//YetAnotherRole = 0b100,

	/** Administrator - includes all below rules */
	Admin = 0b111
}
