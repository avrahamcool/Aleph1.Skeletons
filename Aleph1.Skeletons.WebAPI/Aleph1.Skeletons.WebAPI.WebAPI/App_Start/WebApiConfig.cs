using System.Globalization;
using System.Web.Http;
using System.Web.Http.Cors;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;

using FluentValidation;

using Newtonsoft.Json.Serialization;

using WebApiThrottle;

namespace Aleph1.Skeletons.WebAPI.WebAPI
{
	/// <summary>WebAPI configuration</summary>
	internal static class WebApiConfig
	{
		/// <summary>Registers WebAPI configurations</summary>
		/// <param name="config">Current configuration</param>
		[Logged(LogParameters = false)]
		public static void Register(HttpConfiguration config)
		{
			// WebAPI routes
			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });

			// Cross-origin resource sharing
			if (SettingsManager.EnableCORS)
			{
				config.EnableCors(new EnableCorsAttribute(SettingsManager.Origins, SettingsManager.Headers, SettingsManager.Methods, SettingsManager.ExposedHeaders)
				{
					SupportsCredentials = true
				});
			}

			// JSON field names formatting
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			// Throttling policy, see: https://github.com/stefanprodan/WebApiThrottle
			config.MessageHandlers.Add(new ThrottlingHandler(
				policy: ThrottlePolicy.FromStore(new PolicyConfigurationProvider()),
				policyRepository: null,
				repository: new CacheRepository(),
				logger: null,
				ipAddressParser: new XForwaredIPAddressParser()
			));

			// Model validation attribute
			config.Filters.Add(new ValidatedAttribute());

			// Model validation errors language
			ValidatorOptions.LanguageManager.Culture = new CultureInfo("he");
		}
	}
}
