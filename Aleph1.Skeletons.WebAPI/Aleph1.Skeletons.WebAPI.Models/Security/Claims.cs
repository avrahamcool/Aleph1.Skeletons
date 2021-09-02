using System;

namespace Aleph1.Skeletons.WebAPI.Models.Security
{
	/// <summary>User claims model</summary>
	public class Claims : Identity
	{
		/// <summary>Claims refresh max age</summary>
		/// <remarks>Also handles user identity change while the user is active</remarks>
		public DateTimeOffset RefreshMaxAge { get; set; }

		/// <summary>Claims expiration max age</summary>
		/// <remarks>Claims expiration max age that stops infinite auto-refreshing</remarks>
		public DateTimeOffset ExpirationMaxAge { get; set; }
	}
}
