using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Moq
{
    internal class SecurityMoq : ISecurity
    {
        public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return "MoqTicket";
        }

        public string ReGenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return "MoqTicket";
        }

        public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
        {
            return new AuthenticationInfo() { IsAdmin = true };
        }

        public AuthenticationInfo Login(string username, string password)
        {
            return new AuthenticationInfo() { IsAdmin = true };
        }

        public bool IsAllowedForContent(AuthenticationInfo authenticationInfo, bool RequireAdminAccess)
        {
            return true;
        }
    }
}
