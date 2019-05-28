using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;

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

        internal static T GetHttpParameter<T>(this HttpActionContext context, params string[] parameterNames)
        {
            object possibleValue = null;
            foreach (string parameterName in parameterNames)
            {
                if (parameterName.Contains("."))
                {
                    string[] parameterParts = parameterName.Split('.');
                    if (!context.ActionArguments.ContainsKey(parameterParts[0]))
                        continue;

                    object curentObject = context.ActionArguments[parameterParts[0]];
                    IEnumerable<string> nestedProperties = parameterParts.Skip(1);
                    possibleValue = curentObject.GetPropValue(nestedProperties);
                }
                else if (context.ActionArguments.ContainsKey(parameterName))
                {
                    possibleValue = context.ActionArguments[parameterName];
                    break;
                }
            }

            if (possibleValue != null)
                return (T)possibleValue;

            throw new ArgumentNullException(String.Join(",", parameterNames));
        }

        private static Object GetPropValue(this Object obj, IEnumerable<string> nestedProperties)
        {
            foreach (String part in nestedProperties)
            {
                if (obj == null) { return null; }

                PropertyInfo info = obj.GetType().GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }
    }
}