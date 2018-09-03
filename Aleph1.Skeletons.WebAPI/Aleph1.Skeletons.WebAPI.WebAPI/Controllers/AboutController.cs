using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;
using Aleph1.WebAPI.ExceptionHandler;
using System;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>some data about the current service</summary>
    public class AboutController : ApiController
    {
        /// <summary>Get data about the current api and user</summary>
        [Logged, HttpGet, Route("api/About"), FriendlyMessage("התרחשה שגיאה בעת שליפת נתוני מערכת")]
        public AboutModel About()
        {

            //Client logon Name (when using Windows Authentication)
            //string userUniqueID = HttpContext.Current.User.Identity.Name;
            return new AboutModel()
            {
                ClientIP = HttpContext.Current?.Request?.UserHostAddress,
                ClientUserName = HttpContext.Current?.User?.Identity?.Name,
                Environment = SettingsManager.Environment,
                APIVersion = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString(),
                Server = Environment.MachineName
            };
        }

        /// <summary>Some data about the current api and user</summary>
        public class AboutModel
        {
            /// <summary>client ip.</summary>
            public string ClientIP { get; set; }

            /// <summary>logon name of the client user.</summary>
            public string ClientUserName { get; set; }

            /// <summary>API version. </summary>
            public string APIVersion { get; set; }

            /// <summary>the environment (DEV/TEST/PROD)</summary>
            public string Environment { get; set; }

            /// <summary>server name.</summary>
            public string Server { get; set; }
        }
    }
}
