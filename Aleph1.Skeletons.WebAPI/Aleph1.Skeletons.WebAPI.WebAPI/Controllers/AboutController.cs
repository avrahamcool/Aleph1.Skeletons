using System;
using System.Reflection;
using System.Web.Http;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;
using Aleph1.Skeletons.WebAPI.WebAPI.Models;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Controllers
{
    /// <summary>some data about the current service</summary>
    public class AboutController : ApiController
    {
        /// <summary>get data about the current API and user</summary>
        [Logged, HttpGet, Route("api/about")]
        public AboutModel About()
        {
            return new AboutModel()
            {
                Environment = SettingsManager.Environment,
                APIVersion = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString(),
                Server = SettingsManager.IsProd ? "N/A in Prod" : Environment.MachineName
            };
        }
    }
}
