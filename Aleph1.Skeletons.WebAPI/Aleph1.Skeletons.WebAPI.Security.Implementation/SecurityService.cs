using Aleph1.Logging;
using Aleph1.Security.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

using System;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
    internal class SecurityService : ISecurity
    {
        private readonly ICipher CipherService;

        public SecurityService(ICipher cipherService)
        {
            CipherService = cipherService;
        }

        public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authenticationInfo, SettingsManager.TicketDurationTimeSpan);
        }
        public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
        {
            return ticketValue == default ? null : CipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
        }


        [Logged(LogParameters = false, LogReturnValue = true)]
        public AuthenticationInfo Login(string username, string password)
        {
            // TODO: put your real implementation here
            if (!username.Equals(password, StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException();
            }

            return new AuthenticationInfo() { IsAdmin = username.Equals("admin", StringComparison.OrdinalIgnoreCase), Username = username };
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public bool IsAllowedForContent(AuthenticationInfo authenticationInfo, bool RequireAdminAccess)
        {
            return authenticationInfo != default && (!RequireAdminAccess || authenticationInfo.IsAdmin);
        }
    }
}
