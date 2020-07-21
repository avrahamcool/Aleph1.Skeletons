namespace Aleph1.Skeletons.WebAPI.Models.Security
{
    /// <summary>The information that the server needs for determining if a user is allowed for a resource</summary>
    public class AuthenticationInfo
    {
        // TODO: put your real implementation here

        /// <summary>indicating whether this user has admin access.</summary>
        public bool IsAdmin { get; set; }

        /// <summary>the username</summary>
        public string Username { get; set; }
    }
}
