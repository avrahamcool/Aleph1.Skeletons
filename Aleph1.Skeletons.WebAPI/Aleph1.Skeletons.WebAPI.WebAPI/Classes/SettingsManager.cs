using System;
using System.Configuration;
using System.Globalization;
using System.Linq;

using Aleph1.DI.CustomConfigurationSection;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Classes
{
	/// <summary>Handle settings from web.config</summary>
	internal static class SettingsManager
	{
		private static string _environment;
		public static string Environment
		{
			get
			{
				if (_environment == default)
				{
					_environment = ConfigurationManager.AppSettings["Environment"];
				}
				return _environment;
			}
		}

		public static bool IsProd => Environment.Equals("Prod", StringComparison.OrdinalIgnoreCase);

		private static string _baseModulesDir;
		public static string BaseModulesDir
		{
			get
			{
				if (_baseModulesDir == default)
				{
					_baseModulesDir = (ConfigurationManager.GetSection("Aleph1.DI") as ModulesSection).Base.Path.Trim();
					if (!_baseModulesDir.EndsWith(@"\", StringComparison.InvariantCulture))
					{
						_baseModulesDir += @"\";
					}
				}
				return _baseModulesDir;
			}
		}

		private static string[] _modulesPath;
		public static string[] ModulesPath
		{
			get
			{
				if (_modulesPath == default)
				{
					_modulesPath = (ConfigurationManager.GetSection("Aleph1.DI") as ModulesSection)
						.Modules
						.OfType<ModuleElement>()
						.Select(m => m.Path?.Trim())
						.Where(p => !string.IsNullOrWhiteSpace(p))
						.ToArray();
				}
				return _modulesPath;
			}
		}

		private static string _documentationDirPath;
		public static string DocumentationDirPath
		{
			get
			{
				if (_documentationDirPath == default)
				{
					_documentationDirPath = new Uri(new Uri(AppDomain.CurrentDomain.BaseDirectory), ConfigurationManager.AppSettings["DocumentationDirPath"]).LocalPath;
				}
				return _documentationDirPath;
			}
		}

		private static string _tokenKey;
		public static string TokenKey
		{
			get
			{
				if (_tokenKey == default)
				{
					_tokenKey = ConfigurationManager.AppSettings["TokenKey"];
				}
				return _tokenKey;
			}
		}

		private static int? _claimsInactivityMaxAgeValue;
		private static TimeSpan? _claimsInactivityMaxAge;
		public static TimeSpan? ClaimsInactivityMaxAge
		{
			get
			{
				if (_claimsInactivityMaxAgeValue == default)
				{
					_claimsInactivityMaxAgeValue = int.Parse(ConfigurationManager.AppSettings["ClaimsInactivityMaxAge"], CultureInfo.InvariantCulture);
					if (_claimsInactivityMaxAgeValue.Value != 0)
					{
						_claimsInactivityMaxAge = TimeSpan.FromMinutes(_claimsInactivityMaxAgeValue.Value);
					}
				}
				return _claimsInactivityMaxAge;
			}
		}

		private static bool? _EnableSwagger;
		public static bool EnableSwagger
		{
			get
			{
				if (_EnableSwagger == default)
				{
					_EnableSwagger = bool.Parse(ConfigurationManager.AppSettings["EnableSwagger"]);
				}
				return _EnableSwagger.Value;
			}
		}

		private static bool? _EnableCORS;
		public static bool EnableCORS
		{
			get
			{
				if (_EnableCORS == default)
				{
					_EnableCORS = bool.Parse(ConfigurationManager.AppSettings["EnableCORS"]);
				}
				return _EnableCORS.Value;
			}
		}

		private static string _origins;
		public static string Origins
		{
			get
			{
				if (_origins == default)
				{
					_origins = ConfigurationManager.AppSettings["Origins"];
				}
				return _origins;
			}
		}

		private static string _headers;
		public static string Headers
		{
			get
			{
				if (_headers == default)
				{
					_headers = ConfigurationManager.AppSettings["Headers"];
				}
				return _headers;
			}
		}

		private static string _methods;
		public static string Methods
		{
			get
			{
				if (_methods == default)
				{
					_methods = ConfigurationManager.AppSettings["Methods"];
				}
				return _methods;
			}
		}

		private static string _exposedHeaders;
		public static string ExposedHeaders
		{
			get
			{
				if (_exposedHeaders == default)
				{
					_exposedHeaders = ConfigurationManager.AppSettings["ExposedHeaders"];
				}
				return _exposedHeaders;
			}
		}
	}
}
