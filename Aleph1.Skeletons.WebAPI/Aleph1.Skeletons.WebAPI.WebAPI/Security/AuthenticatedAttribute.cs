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
		private Roles[] RequiredRoles { get; set; }

		/// <summary>Authenticated controller attribute</summary>
		/// <param name="requiredRoles">Minimum required user roles</param>
		public AuthenticatedAttribute(params Roles[] requiredRoles)
		{
			RequiredRoles = requiredRoles.Length == 0 ? new[] { Roles.User } : requiredRoles;
			AllowAnonymous = RequiredRoles.Contains(Roles.None);
		}

		/// <summary>Authenticates the request</summary>
		/// <param name="actionContext">Action context</param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			try
			{
				// Get the DI container for the request scope
				IDependencyScope DI = actionContext.Request.GetDependencyScope();
				ISecurity securityService = DI.GetService(typeof(ISecurity)) as ISecurity;

				// Read authentication claims
				Claims claims = actionContext.GetClaimsFromCookies(securityService);

				if (!AllowAnonymous)
				{
					// Unauthorized if claims timed out
					if (DateTimeOffset.Now > claims.ExpirationMaxAge)
					{
						LogManager.GetCurrentClassLogger().LogAleph1(LogLevel.Warn, $"{claims?.Username ?? "Unknown user"} token timed out");
						actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Token timed out");
						return;
					}

					// Re-evaluate authentication claims
					if (DateTimeOffset.Now > claims.RefreshMaxAge)
					{
						(_, claims) = securityService.CreateIdentityAndClaims(claims.Username, claims.ExpirationMaxAge);
					}

					if (!securityService.IsAllowedForContent(claims, RequiredRoles))
					{
						LogManager.GetCurrentClassLogger().LogAleph1(LogLevel.Warn, $"{claims?.Username ?? "Unknown user"} tried to access {actionContext.Request.RequestUri}");
						actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
						return;
					}
				}

				// Regenerate user claims with the same data to reset time out
				actionContext.Request.SetToken(securityService, claims);
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

		/// <summary>If present, passes the token value from the request to the response</summary>
		/// <param name="actionExecutedContext">Action context</param>
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			string authValue = actionExecutedContext.Request.GetToken();
			actionExecutedContext.AddTokenToCookies(authValue);
		}
	}
}
