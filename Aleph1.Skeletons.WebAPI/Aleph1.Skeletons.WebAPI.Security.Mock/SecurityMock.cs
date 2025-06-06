using System.Threading.Tasks;

using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Mock
{
	internal sealed class SecurityMock : ISecurity
	{
		private readonly AuthenticationInfo MockAuth = new()
		{
			Username = "Mock",
			Roles = Roles.Admin
		};

		public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
		{
			return "MockTicket";
		}

		public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
		{
			return MockAuth;
		}

		public Task<AuthenticationInfo> Login(string username, string password, string captchaToken)
		{
			return Task.FromResult(MockAuth);
		}

		public bool IsAllowedForContent(AuthenticationInfo authenticationInfo, Roles[] allowedForRoles)
		{
			return true;
		}
	}
}
