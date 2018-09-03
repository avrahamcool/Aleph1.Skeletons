using Aleph1.Logging;
using Aleph1.Security.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
    internal class SecurityService : ISecurity
    {
        private readonly ICipher CipherService = null;

        [Logged(LogParameters = false)]
        public SecurityService(ICipher cipherService)
        {
            this.CipherService = cipherService;
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return this.CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authenticationInfo, SettingsManager.TicketDurationTimeSpan);
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public string ReGenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
        {
            return this.CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authenticationInfo, SettingsManager.TicketDurationTimeSpan);
        }

        public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
        {
            return this.CipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public bool IsAllowedForManagementContent(AuthenticationInfo authenticationInfo)
        {
            return authenticationInfo != default(AuthenticationInfo) && authenticationInfo.IsManager;
        }

        [Logged(LogParameters = false, LogReturnValue = true)]
        public bool IsAllowedForRegularContent(AuthenticationInfo authInfo)
        {
            return authInfo != default(AuthenticationInfo);
        }
    }
}
