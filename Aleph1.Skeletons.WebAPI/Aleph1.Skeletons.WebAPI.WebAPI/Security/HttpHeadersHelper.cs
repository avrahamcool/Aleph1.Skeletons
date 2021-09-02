using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using Aleph1.Skeletons.WebAPI.WebAPI.Classes;

using WebApiThrottle.Net;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security
{
	internal static class HttpHeadersHelper
	{
		/// <summary>Implement a logic to identify user uniquely</summary>
		private static string GetUserUniqueID(this HttpRequestMessage request) => request.GetClientIpAddress();

		internal static Claims GetClaimsFromCookies(this HttpRequest request, ISecurity securityService)
		{
			string token = HttpUtility.UrlDecode(request.Cookies.Get(SettingsManager.TokenKey)?.Value);
			return securityService.DecryptToken(token, request.UserHostAddress);
		}
		internal static Claims GetClaimsFromCookies(this HttpActionContext requestAction, ISecurity securityService)
		{
			string token = requestAction.Request.Headers.GetCookies(SettingsManager.TokenKey).FirstOrDefault()?[SettingsManager.TokenKey]?.Value;
			return securityService.DecryptToken(token, requestAction.Request.GetUserUniqueID());
		}
		internal static void AddTokenToCookies(this HttpActionExecutedContext responseAction, string token)
		{
			if (responseAction?.Response == default)
			{
				return;
			}
			CookieHeaderValue cookie = new(SettingsManager.TokenKey, token)
			{
				HttpOnly = true,
				Secure = true,
				Path = SettingsManager.EnableCORS ? "/; SameSite=None" : "/",
				MaxAge = SettingsManager.ClaimsInactivityMaxAge
			};
			responseAction.Response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
		}
		internal static void RemoveTokenFromCookies(this HttpResponseMessage response)
		{
			CookieHeaderValue cookie = new(SettingsManager.TokenKey, "")
			{
				HttpOnly = true,
				Secure = true,
				Path = SettingsManager.EnableCORS ? "/; SameSite=None" : "/",
				MaxAge = TimeSpan.FromSeconds(-1)
			};
			response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
		}

		internal static void SetToken(this HttpRequestMessage request, ISecurity securityService, Claims claims)
		{
			string token = securityService.EncryptClaims(claims, request.GetUserUniqueID());
			request.Properties[SettingsManager.TokenKey] = token;
		}
		internal static string GetToken(this HttpRequestMessage request) => request.Properties.ContainsKey(SettingsManager.TokenKey) ? request.Properties[SettingsManager.TokenKey] as string : null;

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
