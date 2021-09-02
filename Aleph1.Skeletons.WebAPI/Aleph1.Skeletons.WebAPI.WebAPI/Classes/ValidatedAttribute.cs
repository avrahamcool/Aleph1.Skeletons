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
		/// <summary>Validate a given model before invoking the action method</summary>
		/// <param name="actionContext">Action context</param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (!actionContext.ModelState.IsValid)
			{
				actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState);
			}
		}
	}
}
