using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

using NLog;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	internal sealed class AuthenticatedAttribute : ActionFilterAttribute
	{
		private bool AllowAnonymous { get; set; }
		private Roles[] AllowedRoles { get; set; }

		/// <summary></summary>
		/// <param name="AllowedRoles"></param>
		public AuthenticatedAttribute(params Roles[] AllowedRoles)
		{
			// Set default usage to regular user
			this.AllowedRoles = AllowedRoles.Length == 0
				? new[] { Roles.User }
				: AllowedRoles;

			AllowAnonymous = AllowedRoles.Contains(Roles.None);
		}

		/// <summary>Authenticates the request.</summary>
		/// <param name="actionContext">The action context.</param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			try
			{
				// Get the DI container for the request scope
				IDependencyScope DI = actionContext.Request.GetDependencyScope();
				ISecurity securityService = DI.GetService(typeof(ISecurity)) as ISecurity;

				//read the ticket
				AuthenticationInfo authInfo = actionContext.GetAuthenticationInfoFromCookie(securityService);

				if (!AllowAnonymous && !securityService.IsAllowedForContent(authInfo, AllowedRoles))
				{
					LogManager.GetCurrentClassLogger().LogAleph1(LogLevel.Warn, $"{authInfo?.Username ?? "UNKNOWN"} tried to access {actionContext.Request.RequestUri}");
					actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
					return;
				}

				//Regenerating a ticket with the same data - to reset the ticket life span
				actionContext.Request.AddAuthenticationInfo(securityService, authInfo);
			}
			catch (Exception ex)
			{
				if (!AllowAnonymous)
				{
					LogManager.GetCurrentClassLogger().LogAleph1(LogLevel.Warn, actionContext.Request.RequestUri.ToString(), ex);
					actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
				}
			}
		}

		/// <summary>pass the AuthenticationInfo value from the request to the response - if present</summary>
		/// <param name="actionExecutedContext">action context</param>
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			string authValue = actionExecutedContext.Request.GetAuthenticationInfo();
			actionExecutedContext.AddAuthenticationInfoValueToCookie(authValue);
		}
	}
}
