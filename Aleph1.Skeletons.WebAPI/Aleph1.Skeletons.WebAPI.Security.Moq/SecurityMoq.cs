using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Moq
{
    internal class SecurityMoq : ISecurity
    {
        public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return null;
        }

        public string ReGenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return null;
        }

        public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
        {
            return null;
        }

        public bool IsAllowedForRegularContent(AuthenticationInfo authenticationInfo)
        {
            return true;
        }

        public bool IsAllowedForManagementContent(AuthenticationInfo authenticationInfo)
        {
            return true;
        }
    }
}
