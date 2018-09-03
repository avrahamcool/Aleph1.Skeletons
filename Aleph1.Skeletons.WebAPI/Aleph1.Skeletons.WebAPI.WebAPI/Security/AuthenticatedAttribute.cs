using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;
using NLog;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal class AuthenticatedAttribute : ActionFilterAttribute
    {
        public bool AllowAnonymous { get; set; }
        public bool RequireManagerAccess { get; set; }

        #region Security Service
        //has to be injected at run time
        public static ISecurity _securityService = null;
        public static ISecurity SecurityService
        {
            get
            {
                return _securityService ?? throw new NullReferenceException("SecurityService was not injected to the Authenticated Attribute");
            }
        }
        #endregion Security Service

        /// <summary>Authenticates the request.</summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if(!AllowAnonymous)
                {
                    //read the ticket
                    AuthenticationInfo authInfo = actionContext.Request.Headers.GetAuthenticationInfo(SecurityService);

                    //TODO: Check WTE you want using the SecurityService
                    bool canAccess = RequireManagerAccess ?
                        SecurityService.IsAllowedForManagementContent(authInfo) :
                        SecurityService.IsAllowedForRegularContent(authInfo);

                    if (!canAccess)
                        throw new UnauthorizedAccessException();

                    //Regenerating a ticket with the same data - to reset the ticket life span
                    actionContext.Request.Headers.RefreshAuthenticationInfo(SecurityService, authInfo);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().LogAleph1(LogLevel.Warn, actionContext.Request.RequestUri.ToString(), ex);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
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