using Aleph1.Logging;

using System;
using System.Web;
using System.Web.Http;

namespace Aleph1.Skeletons.Proxy.WebAPI
{
    /// <summary>WebAPI Globals</summary>
    /// <seealso cref="HttpApplication" />
    public class WebApiApplication : HttpApplication
    {
        /// <summary>Applications start</summary>
        [Logged(LogParameters = false)]
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        /// <summary>Manage CorrelationID for the logger to use</summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items["CorrelationID"] = Guid.NewGuid();
        }
    }
}
