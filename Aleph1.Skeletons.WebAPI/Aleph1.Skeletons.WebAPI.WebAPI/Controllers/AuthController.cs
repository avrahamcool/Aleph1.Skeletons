using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
	/// <summary>Authentication controller</summary>
	public class AuthController : ApiController
	{
		private readonly ISecurity SecurityService;

		/// <summary>Constructor</summary>
		public AuthController(ISecurity securityService) => SecurityService = securityService;

		/// <summary>Application sign-in</summary>
		/// <param name="credentials">User credentials for signing in</param>
		/// <returns>User identity</returns>
		[Authenticated(Roles.None), Logged(LogParameters = false), HttpPost, Route("api/sign-in")]
		public async Task<Identity> SignIn(Credentials credentials)
		{
			Contract.Requires(credentials != null);
			(Identity identity, Claims claims) = await SecurityService.SignIn(credentials);
			Request.SetToken(SecurityService, claims);
			return identity;
		}

		/// <summary>Refresh access token</summary>
		[Authenticated(Roles.None), Logged, HttpPost, Route("api/refresh-token")]
		public void RefreshToken() { }

		/// <summary>Application sign-out</summary>
		[Logged, HttpPost, Route("api/sign-out")]
		public HttpResponseMessage SignOut()
		{
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
			response.RemoveTokenFromCookies();
			return response;
		}
	}
}
