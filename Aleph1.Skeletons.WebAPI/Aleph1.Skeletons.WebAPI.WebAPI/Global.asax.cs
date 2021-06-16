﻿using System;
using System.Web;
using System.Web.Http;

using Aleph1.Logging;

namespace Aleph1.Skeletons.WebAPI.WebAPI
{
	/// <summary>WebAPI Globals</summary>
	/// <seealso cref="HttpApplication" />
	public class WebApiApplication : HttpApplication
	{
		/// <summary>Applications start</summary>
		[Logged(LogParameters = false)]
		protected void Application_Start() => GlobalConfiguration.Configure(WebApiConfig.Register);

		/// <summary>Manage CorrelationID for the logger to use</summary>
		protected void Application_BeginRequest(object sender, EventArgs e) =>
			//Set a CorrelationID that is unique and consistent across the request.
			HttpContext.Current.Items["CorrelationID"] = Guid.NewGuid();
	}
}
