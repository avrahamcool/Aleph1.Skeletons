export enum Roles
	{
	None = 0, // Not signed in
	User = 1 << 0, // Regular user
	SomeOtherRole = 1 << 1, // Some other role
	Admin = None | User | SomeOtherRole // Includes all of the above roles
}
