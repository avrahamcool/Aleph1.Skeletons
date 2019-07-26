using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Models;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;
using Aleph1.WebAPI.ExceptionHandler;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>handle login</summary>
    public class LoginController : ApiController
    {
        private readonly ISecurity SecurityService;

        /// <summary>Initializes a new instance of the <see cref="LoginController"/> class.</summary>
        [Logged(LogParameters = false)]
        public LoginController(ISecurity securityService)
        {
            SecurityService = securityService;
        }

        /// <summary>Login to the APP (use same user and password for successful login. use admin for manager).</summary>
        /// <param name="loginModel">Credentials for login</param>
        [Authenticated(AllowAnonymous = true), Logged(LogParameters = false), HttpPost, Route("api/Login"), FriendlyMessage("התרחשה שגיאה בעת ההתחברות")]
        public string Login(LoginModel loginModel)
        {
            AuthenticationInfo auth = SecurityService.Login(loginModel.Username, loginModel.Password);
            return Request.Headers.AddAuthenticationInfo(SecurityService, auth);
        }
    }
}
