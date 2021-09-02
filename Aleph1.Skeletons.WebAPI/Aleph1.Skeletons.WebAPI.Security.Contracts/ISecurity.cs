using System;
using System.Threading.Tasks;

using Aleph1.Skeletons.WebAPI.Models.Security;

namespace Aleph1.Skeletons.WebAPI.Security.Contracts
{
	/// <summary>Security layer (authentication/authorization)</summary>
	public interface ISecurity
	{
		/// <summary>Encrypts user identity</summary>
		/// <param name="claims">User claims to be encrypted</param>
		/// <param name="signature">Encryption signature</param>
		/// <returns>Encrypted token</returns>
		string EncryptClaims(Claims claims, string signature);

		/// <summary>Decrypts an encrypted token</summary>
		/// <param name="token">Encrypted token</param>
		/// <param name="signature">Encryption signature</param>
		/// <returns>Decrypted user claims</returns>
		Claims DecryptToken(string token, string signature);

		/// <summary>Creates user identity and user claims</summary>
		/// <param name="username">User name</param>
		/// <param name="expirationMaxAge">Maximum Claims time out</param>
		/// <returns>User identity and user claims</returns>
		(Identity, Claims) CreateIdentityAndClaims(string username, DateTimeOffset expirationMaxAge);

		/// <summary>Creates user identity and claims from given credentials</summary>
		/// <param name="credentials">User credentials</param>
		/// <returns>User identity and claims</returns>
		Task<(Identity, Claims)> SignIn(Credentials credentials);

		/// <summary>Checks whether the current user is allowed for content</summary>
		/// <param name="claims">User claims</param>
		/// <param name="requiredRoles">Minimum required roles to access a selected resource</param>
		/// <returns>True if allowed, false otherwise</returns>
		bool IsAllowedForContent(Claims claims, Roles[] requiredRoles);
	}
}
