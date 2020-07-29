// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Catching all kind of exceptions", Scope = "member", Target = "~M:Aleph1.Skeletons.WebAPI.Security.Implementation.SecurityService.ReadTicket(System.String,System.String)~Aleph1.Skeletons.WebAPI.Models.Security.AuthenticationInfo")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Injected class", Scope = "type", Target = "~T:Aleph1.Skeletons.WebAPI.Security.Implementation.SecurityService")]
