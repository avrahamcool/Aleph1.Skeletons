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
	internal class SecurityService : ISecurity
	{
		private readonly ICipher CipherService;
		private readonly ICaptcha Captcha;

		public SecurityService(ICipher cipherService, ICaptcha captcha)
		{
			CipherService = cipherService;
			Captcha = captcha;
		}

		public string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID)
		{
			return CipherService.Encrypt(SettingsManager.AppPrefix, userUniqueID, authenticationInfo, SettingsManager.TicketDurationTimeSpan);
		}
		public AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID)
		{
			try
			{
				return CipherService.Decrypt<AuthenticationInfo>(SettingsManager.AppPrefix, userUniqueID, ticketValue);
			}
			catch
			{
				return null;
			}
		}


		[Logged(LogParameters = false, LogReturnValue = true)]
		public async Task<AuthenticationInfo> Login(string username, string password, string captchaToken)
		{
			await Captcha.ValidateCaptcha(captchaToken);

			// TODO: put your real implementation here
			if (!username.Equals(password, StringComparison.OrdinalIgnoreCase))
			{
				throw new UnauthorizedAccessException();
			}

			return new AuthenticationInfo()
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
