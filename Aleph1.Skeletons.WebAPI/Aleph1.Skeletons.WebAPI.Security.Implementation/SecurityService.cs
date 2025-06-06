using System;
using System.Linq;
using System.Threading.Tasks;

using Aleph1.Logging;
using Aleph1.Security.Contracts;
using Aleph1.Skeletons.WebAPI.Captcha.Contracts;
using Aleph1.Skeletons.WebAPI.Models.Security;
using Aleph1.Skeletons.WebAPI.Security.Contracts;

namespace Aleph1.Skeletons.WebAPI.Security.Implementation
{
	internal sealed class SecurityService(ICipher cipherService, ICaptcha captcha) : ISecurity
	{
		public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
		{
			return cipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authenticationInfo, SettingsManager.TicketDurationTimeSpan);
		}

		public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
		{
			try
			{
				return cipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
			}
			catch
			{
				return null;
			}
		}


		[Logged(LogParameters = false, LogReturnValue = true)]
		public async Task<AuthenticationInfo> Login(string username, string password, string captchaToken)
		{
			await captcha.ValidateCaptcha(captchaToken).ConfigureAwait(false);

			// TODO: put your real implementation here
			return !username.Equals(password, StringComparison.OrdinalIgnoreCase)
				? throw new UnauthorizedAccessException()
				: new AuthenticationInfo()
				{
					Username = username,
					Roles = username.Equals("admin", StringComparison.OrdinalIgnoreCase) ? Roles.Admin : Roles.User
				};
		}

		[Logged(LogParameters = false, LogReturnValue = true)]
		public bool IsAllowedForContent(AuthenticationInfo authenticationInfo, Roles[] allowedForRoles)
		{
			return authenticationInfo != default && allowedForRoles.Any(r => authenticationInfo.Roles.HasFlag(r));
		}
	}
}
