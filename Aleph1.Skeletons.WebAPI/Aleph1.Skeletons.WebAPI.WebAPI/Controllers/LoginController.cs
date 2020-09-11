using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Models;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;
using Aleph1.WebAPI.ExceptionHandler;

using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>handle login</summary>
    public class LoginController : ApiController
    {
        private readonly ISecurity SecurityService;

        /// <summary></summary>
        public LoginController(ISecurity securityService)
        {
            SecurityService = securityService;
        }

        /// <summary>Login to the APP (use same user and password for successful login. use 'admin' 'admin' for manager).</summary>
        /// <param name="loginModel">Credentials for login</param>
        [Authenticated(Roles.Anonymous), Logged(LogParameters = false), HttpPost, Route("api/Login"), FriendlyMessage("התרחשה שגיאה בעת ההתחברות")]
        public AuthenticationInfo Login(LoginModel loginModel)
        {
            Contract.Requires(loginModel != null);

            AuthenticationInfo authenticationInfo = SecurityService.Login(loginModel.Username, loginModel.Password);
            Request.AddAuthenticationInfo(SecurityService, authenticationInfo);

            return authenticationInfo;
        }

        /// <summary>refresh access token.</summary>
        [Authenticated(Roles.Anonymous), Logged, HttpGet, Route("api/RefreshToken")]
        public void RefreshToken() { }

        /// <summary>Logout from the application</summary>
        [Logged, HttpPost, Route("api/Logout"), FriendlyMessage("התרחשה שגיאה בעת ההתנתקות")]
        public HttpResponseMessage Logout()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);
            response.RemoveAuthenticationInfoValueFromCookie();

            return response;
        }
    }
}
