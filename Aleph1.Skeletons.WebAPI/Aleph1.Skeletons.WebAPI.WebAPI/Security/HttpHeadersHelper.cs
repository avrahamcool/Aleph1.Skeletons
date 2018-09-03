using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security
{
    internal static class HttpHeadersHelper
    {
        /// <summary>Implement a logic to identify user uniquely</summary>
        private static string GetUserUniqueID(this HttpRequest httpRequest) => httpRequest.UserHostAddress;

        public static string GetAuthenticationInfoValue(this HttpRequestHeaders headers)
        {
            return headers.FirstOrDefault(h => h.Key.Equals(SettingsManager.AuthenticationHeaderKey, StringComparison.OrdinalIgnoreCase)).Value?.FirstOrDefault();
        }
        public static string AddAuthenticationInfoValue(this HttpHeaders headers, string value)
        {
            if (headers.Contains(SettingsManager.AuthenticationHeaderKey))
                headers.Remove(SettingsManager.AuthenticationHeaderKey);

            if (!String.IsNullOrWhiteSpace(value))
                headers.Add(SettingsManager.AuthenticationHeaderKey, value);

            return value;
        }


        internal static AuthenticationInfo GetAuthenticationInfo(this HttpRequestHeaders headers, ISecurity securityService)
        {
            return securityService.ReadTicket(headers.GetAuthenticationInfoValue(), HttpContext.Current.Request.GetUserUniqueID());
        }
        internal static string AddAuthenticationInfo(this HttpHeaders headers, ISecurity securityService, AuthenticationInfo authInfo)
        {
            return headers.AddAuthenticationInfoValue(securityService.GenerateTicket(authInfo, HttpContext.Current.Request.GetUserUniqueID()));
        }
        internal static string RefreshAuthenticationInfo(this HttpRequestHeaders headers, ISecurity securityService, AuthenticationInfo authInfo)
        {
            return headers.AddAuthenticationInfoValue(securityService.ReGenerateTicket(authInfo, HttpContext.Current.Request.GetUserUniqueID()));
        }
    }
}