namespace Aleph1.Skeletons.WebAPI.Models.Security
{
	/// <summary>User identity</summary>
	public class Identity
	{
		/// <summary>User name</summary>
		public string Username { get; set; }

		/// <summary>User roles</summary>
		public Roles Roles { get; set; }
	}
}
