namespace Aleph1.Skeletons.WebAPI.WebAPI.Models
{
	/// <summary>Some data about the current API and user</summary>
	public class AboutModel
	{
		/// <summary>API version</summary>
		public string APIVersion { get; set; }

		/// <summary>the environment (DEV/TEST/PROD)</summary>
		public string Environment { get; set; }

		/// <summary>server name</summary>
		public string Server { get; set; }
	}
}
