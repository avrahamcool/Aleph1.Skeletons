using System;
using System.Threading.Tasks;

using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Mock
{
	internal class SecurityMock : ISecurity
	{
		private readonly Identity Identity = new()
		{
			Username = "john",
			Roles = Roles.Admin
		};

		private readonly Claims Claims = new()
		{
			Username = "john",
			Roles = Roles.Admin
		};

		public string EncryptClaims(Claims claims, string signature) => "claims";

		public Claims DecryptToken(string token, string signature) => Claims;

		public (Identity, Claims) CreateIdentityAndClaims(string username, DateTimeOffset expirationMaxAge) => (Identity, Claims);

		public Task<(Identity, Claims)> SignIn(Credentials credentials) => Task.FromResult((Identity, Claims));

		public bool IsAllowedForContent(Claims claims, Roles[] requiredRoles) => true;
	}
}
