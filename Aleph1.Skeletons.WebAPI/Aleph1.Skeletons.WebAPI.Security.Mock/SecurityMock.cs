using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Mock
{
    internal class SecurityMock : ISecurity
    {
        public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return "MockTicket";
        }

        public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
        {
            return new AuthenticationInfo() { IsAdmin = true, Username = "Mock" };
        }

        public AuthenticationInfo Login(string username, string password)
        {
            return new AuthenticationInfo() { IsAdmin = true, Username = "Mock" };
        }

        public bool IsAllowedForContent(AuthenticationInfo authenticationInfo, bool RequireAdminAccess)
        {
            return true;
        }
    }
}
