using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Models;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
	/// <summary>handle login</summary>
	public class LoginController : ApiController
	{
		private readonly ISecurity SecurityService;

		/// <summary></summary>
		public LoginController(ISecurity securityService) => SecurityService = securityService;

		/// <summary>login to the APP (use same user and password for successful login. use 'admin' 'admin' for manager).</summary>
		/// <param name="loginModel">credentials for login</param>
		[Authenticated(Roles.None), Logged(LogParameters = false), HttpPost, Route("api/login")]
		public async Task<AuthenticationInfo> Login(LoginModel loginModel)
		{
			Contract.Requires(loginModel != null);

			AuthenticationInfo authenticationInfo = await SecurityService.Login(loginModel.Username, loginModel.Password, loginModel.CaptchaToken);
			Request.AddAuthenticationInfo(SecurityService, authenticationInfo);

			return authenticationInfo;
		}

		/// <summary>refresh access token.</summary>
		[Authenticated(Roles.None), Logged, HttpPost, Route("api/refresh-token")]
		public void RefreshToken() { }

		/// <summary>logout from the application</summary>
		[Logged, HttpPost, Route("api/logout")]
		public HttpResponseMessage Logout()
		{
			HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
			response.RemoveAuthenticationInfoValueFromCookie();

			return response;
		}
	}
}
