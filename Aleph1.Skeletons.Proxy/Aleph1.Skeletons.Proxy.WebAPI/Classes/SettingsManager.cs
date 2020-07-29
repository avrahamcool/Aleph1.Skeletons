using Aleph1.DI.CustomConfigurationSection;

using System;
using System.Configuration;
using System.Linq;

namespace Aleph1.Skeletons.Proxy.WebAPI.Classes
{
    /// <summary>Handle settings from config</summary>
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
    }
}