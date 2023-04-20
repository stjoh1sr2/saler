using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Controllers
{
    /// <summary>
    /// Handles operations for users.
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// Logs out the current user in session.
        /// </summary>
        /// <returns>Redirect to home page.</returns>
        public ActionResult LogOut()
        {
            // Ensure that a user is logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                // Log out user
                HttpContext.Session.Remove("_User");
            }

            // Redirect to home page
            return Redirect("~/");
        }
    }
}
