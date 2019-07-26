using Aleph1.Skeletons.WebAPI.Models.Security;

namespace Aleph1.Skeletons.WebAPI.Security.Contracts
{
    /// <summary>Handles security (authentication/authorization)</summary>
    public interface ISecurity
    {
        #region GeneralAuth
        /// <summary>create an encrypted ticked that includes all the AuthenticationInfo - can only be opened for a specific user</summary>
        /// <param name="authenticationInfo">the data to encrypt</param>
        /// <param name="userUniqueID">ticket will be open-able for this user only</param>
        /// <returns>encrypted ticket</returns>
        string GenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID);

        /// <summary>extend the duration of a ticket</summary>
        /// <param name="authenticationInfo">the data to encrypt</param>
        /// <param name="userUniqueID">ticket will be open-able for this user only</param>
        /// <returns></returns>
        string ReGenerateTicket(AuthenticationInfo authenticationInfo, string userUniqueID);

        /// <summary>decrypt a ticket</summary>
        /// <param name="ticketValue">the encrypted ticket</param>
        /// <param name="userUniqueID">the user that this ticket was encrypted to</param>
        /// <returns>the data</returns>
        AuthenticationInfo ReadTicket(string ticketValue, string userUniqueID);
        #endregion GeneralAuth

        /// <summary>Generate a AuthenticationInfo based on the given credentials</summary>
        /// <param name="username">the username</param>
        /// <param name="password">the password</param>
        /// <returns>an AuthenticationInfo representing the credentials of the user</returns>
        AuthenticationInfo Login(string username, string password);

        /// <summary>return true if the current user is allowed for content marked as regular</summary>
        /// <param name="authenticationInfo">the user authentication info</param>
        /// <returns>true if allowed, false otherwise</returns>
        bool IsAllowedForRegularContent(AuthenticationInfo authenticationInfo);

        /// <summary>return true if the current user is allowed for content marked as management</summary>
        /// <param name="authenticationInfo">the user authentication info</param>
        /// <returns>true if allowed, false otherwise</returns>
        bool IsAllowedForManagementContent(AuthenticationInfo authenticationInfo);
    }
}
