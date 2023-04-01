using System.ComponentModel.DataAnnotations.Schema;

namespace salerapp.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }

        [NotMapped]
        public bool LoggedIn { get; set; } = false;
    }
}
