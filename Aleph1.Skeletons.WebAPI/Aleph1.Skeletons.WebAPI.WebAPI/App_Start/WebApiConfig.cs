using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;
using Aleph1.Skeletons.WebAPI.WebAPI.Security;
using FluentValidation;
using System.Globalization;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiThrottle;

namespace Aleph1.Skeletons.WebAPI.WebAPI
{
    /// <summary>web api congigurations</summary>
    internal static class WebApiConfig
    {
        /// <summary>Registers web api configurations</summary>
        /// <param name="config">The current configuration</param>
        [Logged(LogParameters = false)]
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

            if (SettingsManager.EnableCORS)
            {
                config.EnableCors(new EnableCorsAttribute(SettingsManager.Origins, SettingsManager.Headers, SettingsManager.Methods, SettingsManager.ExposedHeaders));
            }

            //Apply Throttling Policy on all Controllers - from web.config
            //see more configs here: https://github.com/stefanprodan/WebApiThrottle
            config.MessageHandlers.Add(new ThrottlingHandler(
                policy: ThrottlePolicy.FromStore(new PolicyConfigurationProvider()),
                policyRepository: null,
                repository: new CacheRepository(),
                logger: null,
                ipAddressParser: new XForwaredIPAddressParser()
            ));

            //Apply model validation attribute to all controllers
            config.Filters.Add(new ValidatedAttribute());

            //Configure Model validation errors to be in Hebrew
            ValidatorOptions.LanguageManager.Culture = new CultureInfo("he");
        }
    }
}
