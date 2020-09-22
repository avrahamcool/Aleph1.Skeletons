using Aleph1.DI.CustomConfigurationSection;

using System;
using System.Configuration;
using System.Globalization;
using System.Linq;

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

        private static string _authenticationHeaderKey;
        public static string AuthenticationHeaderKey
        {
            get
            {
                if (_authenticationHeaderKey == default)
                {
                    _authenticationHeaderKey = ConfigurationManager.AppSettings["AuthenticationHeaderKey"];
                }
                return _authenticationHeaderKey;
            }
        }

        private static int? _ticketDurationMin;
        private static TimeSpan? _ticketDurationTimeSpan;
        public static TimeSpan? TicketDurationTimeSpan
        {
            get
            {
                if (_ticketDurationMin == default)
                {
                    _ticketDurationMin = int.Parse(ConfigurationManager.AppSettings["TicketDurationMin"], CultureInfo.InvariantCulture);
                    if (_ticketDurationMin.Value != 0)
                    {
                        _ticketDurationTimeSpan = TimeSpan.FromMinutes(_ticketDurationMin.Value);
                    }
                }
                return _ticketDurationTimeSpan;
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