using System;

namespace Aleph1.Skeletons.WebAPI.Models.Security
{
	/// <summary>User roles</summary>
	[Flags]
	public enum Roles
	{
		/// <summary>Not signed in</summary>
		None = 0,

		/// <summary>Regular user</summary>
		User = 1 << 0,

		/// <summary>Some other role</summary>
		SomeOtherRole = 1 << 1,

		/// <summary>Includes all of the above roles</summary>
		Admin = None | User | SomeOtherRole
	}
}
