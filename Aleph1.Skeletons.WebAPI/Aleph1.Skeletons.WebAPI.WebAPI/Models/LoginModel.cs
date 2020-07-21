namespace Aleph1.Skeletons.WebAPI.WebAPI.Models
{
    /// <summary>Credentials for login</summary>
    public class LoginModel
    {
        /// <summary>The user name (length must be 2-10)</summary>
        public string Username { get; set; }

        /// <summary>The password (length must be 2-10)</summary>
        public string Password { get; set; }
    }
}