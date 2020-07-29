using System;
using System.Configuration;

namespace Aleph1.Skeletons.Proxy.Proxy.Implementation
{
    internal static class SettingsManager
    {
        private static Uri _ServiceBaseUrl;
        public static Uri ServiceBaseUrl
        {
            get
            {
                if (_ServiceBaseUrl == default)
                {
                    string baseUrlString = ConfigurationManager.AppSettings["ServiceBaseUrl"];
                    if (string.IsNullOrWhiteSpace(baseUrlString))
                    {
                        throw new ArgumentNullException("ServiceBaseUrl");
                    }

                    if (!baseUrlString.EndsWith("/", StringComparison.InvariantCulture))
                    {
                        baseUrlString += '/';
                    }

                    _ServiceBaseUrl = new Uri(baseUrlString);
                }
                return _ServiceBaseUrl;
            }
        }
    }
}
