using System;
using System.Reflection;
using System.Web.Http;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Models;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
	/// <summary>About controller</summary>
	public class AboutController : ApiController
	{
		/// <summary>Get data about the current API and user</summary>
		[Logged, HttpGet, Route("api/about")]
		public About About()
		{
			return new About()
			{
				Environment = SettingsManager.Environment,
				ApiVersion = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString(),
				Server = SettingsManager.IsProd ? "N/A in Prod" : Environment.MachineName
			};
		}
	}
}
