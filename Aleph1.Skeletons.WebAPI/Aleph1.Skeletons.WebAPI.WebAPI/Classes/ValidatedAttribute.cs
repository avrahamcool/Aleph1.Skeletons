using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Aleph1.Skeletons.WebAPI.WebAPI.Classes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	internal sealed class ValidatedAttribute : ActionFilterAttribute
	{
		/// <summary>check model validity before the action method is invoked.</summary>
		/// <param name="actionContext">The action context.</param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (!actionContext.ModelState.IsValid)
			{
				actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
			}
		}
	}
}