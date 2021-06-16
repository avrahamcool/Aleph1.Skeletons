using System.Threading.Tasks;

using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Mock
{
	internal class SecurityMock : ISecurity
	{
		private readonly AuthenticationInfo MockAuth = new()
		{
			Username = "Mock",
			Roles = Roles.Admin
		};

		public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID) => "MockTicket";

		public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID) => MockAuth;

		public Task<AuthenticationInfo> Login(string username, string password, string captchaToken) => Task.FromResult(MockAuth);

		public bool IsAllowedForContent(AuthenticationInfo authenticationInfo, Roles[] allowedForRoles) => true;
	}
}
