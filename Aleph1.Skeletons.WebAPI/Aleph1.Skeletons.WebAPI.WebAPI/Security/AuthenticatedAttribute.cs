using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Filters;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal sealed class AuthenticatedAttribute : ActionFilterAttribute
    {
        public bool AllowAnonymous { get; set; }
        public bool RequireManagerAccess { get; set; }

        /// <summary>Authenticates the request.</summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (!AllowAnonymous)
                {
                    // Get the DI container for the request scope
                    IDependencyScope DI = actionContext.Request.GetDependencyScope();
                    ISecurity securityService = DI.GetService(typeof(ISecurity)) as ISecurity;

                    //read the ticket
                    AuthenticationInfo authInfo = actionContext.Request.Headers.GetAuthenticationInfo(securityService);

                    //TODO: Check WTE you want using the SecurityService
                    bool canAccess = RequireManagerAccess ?
                        securityService.IsAllowedForManagementContent(authInfo) :
                        securityService.IsAllowedForRegularContent(authInfo);

                    if (!canAccess)
                        throw new UnauthorizedAccessException("Your ticket does not have sufficient permissions for this action.");

                    //Regenerating a ticket with the same data - to reset the ticket life span
                    actionContext.Request.Headers.RefreshAuthenticationInfo(securityService, authInfo);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().LogAleph1(LogLevel.Warn, actionContext.Request.RequestUri.ToString(), ex);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex);
            }
        }

        /// <summary>pass the AuthenticationInfo value from the request to the response - if present</summary>
        /// <param name="actionExecutedContext">action context</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            string authValue = actionExecutedContext.Request.Headers.GetAuthenticationInfoValue();
            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.AddAuthenticationInfoValue(authValue);
            }
        }
    }
}