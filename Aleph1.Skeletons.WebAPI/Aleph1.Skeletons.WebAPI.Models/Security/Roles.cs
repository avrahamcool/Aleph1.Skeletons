using System;

namespace Aleph1.Skeletons.WebAPI.Models.Security
{
    /// <summary>Authorization roles</summary>
    [Flags]
    public enum Roles
    {
        /// <summary>Not logged in</summary>
        Anonymous = 0b000,

        /// <summary>Regular user</summary>
        User = 0b001,

        //AnotherRole = 0b010,
        //YetAnotherRole = 0b100,

        /// <summary>Administrator</summary>
        /// <remarks>includes all below rules</remarks>
        Admin = 0b111
    }
}
