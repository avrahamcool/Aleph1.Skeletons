using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using WebApiThrottle.Net;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security
{
    internal static class HttpHeadersHelper
    {
        /// <summary>Implement a logic to identify user uniquely</summary>
        private static string GetUserUniqueID(this HttpRequestMessage request)
        {
            return request.GetClientIpAddress();
        }

        internal static AuthenticationInfo GetAuthenticationInfoFromCookie(this HttpRequest request, ISecurity securityService)
        {
            string ticket = HttpUtility.UrlDecode(request.Cookies.Get(SettingsManager.AuthenticationHeaderKey)?.Value);
            return securityService.ReadTicket(ticket, request.UserHostAddress);
        }
        internal static AuthenticationInfo GetAuthenticationInfoFromCookie(this HttpActionContext requestAction, ISecurity securityService)
        {
            string ticket = requestAction.Request.Headers.GetCookies(SettingsManager.AuthenticationHeaderKey).FirstOrDefault()?[SettingsManager.AuthenticationHeaderKey]?.Value;
            return securityService.ReadTicket(ticket, requestAction.Request.GetUserUniqueID());
        }
        internal static void AddAuthenticationInfoValueToCookie(this HttpActionExecutedContext responseAction, string value)
        {
            if (responseAction?.Response == default)
            {
                return;
            }

            CookieHeaderValue cookie = new CookieHeaderValue(SettingsManager.AuthenticationHeaderKey, value)
            {
                HttpOnly = true,
                Secure = true,
                Path = "/",
                MaxAge = SettingsManager.TicketDurationTimeSpan
            };
            responseAction.Response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        }

        internal static void RemoveAuthenticationInfoValueFromCookie(this HttpResponseMessage response)
        {
            CookieHeaderValue cookie = new CookieHeaderValue(SettingsManager.AuthenticationHeaderKey, "")
            {
                HttpOnly = true,
                Secure = true,
                Path = "/",
                MaxAge = TimeSpan.FromSeconds(-1)
            };
            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        }

        internal static void AddAuthenticationInfo(this HttpRequestMessage request, ISecurity securityService, AuthenticationInfo authInfo)
        {
            string ticket = securityService.GenerateTicket(authInfo, request.GetUserUniqueID());
            request.Properties[SettingsManager.AuthenticationHeaderKey] = ticket;
        }
        internal static string GetAuthenticationInfo(this HttpRequestMessage request)
        {
            return request.Properties.ContainsKey(SettingsManager.AuthenticationHeaderKey) ? request.Properties[SettingsManager.AuthenticationHeaderKey] as string : null;
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
                    {
                        continue;
                    }

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
            {
                return (T)possibleValue;
            }

            throw new ArgumentNullException(string.Join(",", parameterNames));
        }
        private static object GetPropValue(this object obj, IEnumerable<string> nestedProperties)
        {
            foreach (string part in nestedProperties)
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