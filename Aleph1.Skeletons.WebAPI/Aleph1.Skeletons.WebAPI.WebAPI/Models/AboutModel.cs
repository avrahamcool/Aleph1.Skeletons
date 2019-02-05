namespace Aleph1.Skeletons.WebAPI.WebAPI.Models
{
    /// <summary>Some data about the current api and user</summary>
    public class AboutModel
    {
        /// <summary>client ip.</summary>
        public string ClientIP { get; set; }

        /// <summary>logon name of the client user.</summary>
        public string ClientUserName { get; set; }

        /// <summary>API version. </summary>
        public string APIVersion { get; set; }

        /// <summary>the environment (DEV/TEST/PROD)</summary>
        public string Environment { get; set; }

        /// <summary>server name.</summary>
        public string Server { get; set; }
    }
}