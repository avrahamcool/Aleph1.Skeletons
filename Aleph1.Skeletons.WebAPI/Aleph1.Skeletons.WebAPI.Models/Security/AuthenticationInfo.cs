namespace Aleph1.Skeletons.WebAPI.Models.Security
{
    /// <summary>The information that the server needs for determining if a user is allowed for a resource</summary>
    public class AuthenticationInfo
    {
        /// <summary>indicating whether this user has manager access.</summary>
        public bool IsManager { get; set; }
    }
}
