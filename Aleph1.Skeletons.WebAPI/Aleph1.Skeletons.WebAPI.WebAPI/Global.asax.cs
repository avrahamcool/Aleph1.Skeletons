using Aleph1.Logging;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;

namespace Aleph1.Skeletons.WebAPI.WebAPI
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

        /// <summary>Strip Server Headers</summary>
        protected void Application_PreSendRequestHeaders()
        {
            //Those headers are not removed when set in config
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
        }

        /// <summary>Manage CorrelationID for the logger to use</summary>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //Create a single CorrelationID for all the messages in the same request life cycle
            Trace.CorrelationManager.ActivityId = Guid.NewGuid();

            //setting the ActivityId to Guid.Empty will cause the logger to generate a CorrelationID for each function separately
            //Trace.CorrelationManager.ActivityId = Guid.Empty;
        }
    }
}
