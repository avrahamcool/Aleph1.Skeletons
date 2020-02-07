// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Application_Start Cannot be marked as static", Scope = "member", Target = "~M:Aleph1.Skeletons.WebAPI.WebAPI.WebApiApplication.Application_Start")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Action Cannot be marked as static", Scope = "member", Target = "~M:Aleph1.Skeletons.WebAPI.WebAPI.Controllers.AboutController.About~Aleph1.Skeletons.WebAPI.WebAPI.Models.AboutModel")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Catching all kind of exceptions", Scope = "member", Target = "~M:Aleph1.Skeletons.WebAPI.WebAPI.Security.AuthenticatedAttribute.OnActionExecuting(System.Web.Http.Controllers.HttpActionContext)")]
