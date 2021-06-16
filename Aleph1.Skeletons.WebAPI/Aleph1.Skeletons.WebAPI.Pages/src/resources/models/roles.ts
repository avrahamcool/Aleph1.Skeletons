/** Authorization roles */
export enum Roles
{
	/** Not logged in */
	None = 0,

	/** Regular user */
	User = 1 << 0,

	/** Another role for demonstration  */
	SomeOtherRole = 1 << 1,

	/** Administrator - includes all below rules */
	Admin = None | User | SomeOtherRole
}
