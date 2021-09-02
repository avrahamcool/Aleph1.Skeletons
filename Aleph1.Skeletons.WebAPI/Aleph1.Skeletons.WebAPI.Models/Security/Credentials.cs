namespace Aleph1.Skeletons.WebAPI.Models.Security
{
	/// <summary>Sign-in credentials model</summary>
	public class Credentials
	{
		/// <summary>User name</summary>
		public string Username { get; set; }

		/// <summary>User password</summary>
		public string Password { get; set; }

		/// <summary>User CAPTCHA token</summary>
		public string Captcha { get; set; }
	}
}
