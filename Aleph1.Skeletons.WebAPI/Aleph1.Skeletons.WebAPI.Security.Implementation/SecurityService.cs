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

		public string EncryptClaims(Claims claims, string signature) => CipherService.Encrypt(SettingsManager.AppPrefix, signature, claims, SettingsManager.ClaimsInactivityMaxAge);

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Need a catch-all block")]
		public Claims DecryptToken(string token, string signature)
		{
			try
			{
				return CipherService.Decrypt<Claims>(SettingsManager.AppPrefix, signature, token);
			}
			catch
			{
				return null;
			}
		}

		public (Identity, Claims) CreateIdentityAndClaims(string username, DateTimeOffset expirationMaxAge)
		{
			// TODO: Put your real implementation here!

			var roles = username.Equals("admin", StringComparison.OrdinalIgnoreCase) ? Roles.Admin : Roles.User;

			Identity identity = new()
			{
				Username = username,
				Roles = roles
			};

			Claims claims = new()
			{
				Username = username,
				Roles = roles,
				RefreshMaxAge = DateTimeOffset.Now + SettingsManager.ClaimsRefreshMaxAge,
				ExpirationMaxAge = expirationMaxAge
			};

			return (identity, claims);
		}

		[Logged(LogParameters = false, LogReturnValue = true)]
		public async Task<(Identity, Claims)> SignIn(Credentials credentials)
		{
			await Captcha.ValidateCaptcha(credentials.Captcha).ConfigureAwait(false);

			// TODO: Put your real implementation here!

			if (!credentials.Username.Equals(credentials.Password, StringComparison.OrdinalIgnoreCase))
			{
				throw new UnauthorizedAccessException();
			}

			return CreateIdentityAndClaims(credentials.Username, DateTimeOffset.Now + SettingsManager.ClaimsExpirationMaxAge);
		}

		[Logged(LogParameters = false, LogReturnValue = true)]
		public bool IsAllowedForContent(Claims claims, Roles[] requiredRoles) => claims != default && requiredRoles.Any(r => claims.Roles.HasFlag(r));
	}
}
