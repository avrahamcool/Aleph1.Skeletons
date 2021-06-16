using System;

namespace Aleph1.Skeletons.WebAPI.Models.Security
{
	/// <summary>Authorization roles</summary>
	[Flags]
	public enum Roles
	{
		/// <summary>Not logged in</summary>
		None = 0,

		/// <summary>Regular user</summary>
		User = 1 << 0,

		/// <summary>Another role for demonstration</summary>
		SomeOtherRole = 1 << 1,

		/// <summary>Administrator</summary>
		/// <remarks>includes all below rules</remarks>
		Admin = None | User | SomeOtherRole
	}
}
