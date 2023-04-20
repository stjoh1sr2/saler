using System.ComponentModel.DataAnnotations.Schema;

namespace salerapp.Models
{
    /// <summary>
    /// A Saler user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// The primary ID of the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The user's public username.
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Flags whether or not the user is currently logged in.
        /// </summary>
        [NotMapped]
        public bool LoggedIn { get; set; } = false;
    }
}
