using Microsoft.AspNetCore.Mvc;
using salerapp.Models;

namespace salerapp.Helpers
{
    public class UserManagementHelper
    {
        public static User GlobalUser { get; set; } = new User();

        public static void LogOut()
        {
            GlobalUser = new User();
        }
    }
}
