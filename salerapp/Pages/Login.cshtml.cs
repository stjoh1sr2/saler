using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    /// <summary>
    /// PageModel for the Login page.
    /// </summary>
    public class LoginModel : PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// The username or email address for the user.
        /// </summary>
        public String identification { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public String password { get; set; }

        /// <summary>
        /// Warnings for the user logging in.
        /// </summary>
        public List<String> warnings = new List<String>();

        /// <summary>
        /// Submits the login attempt for validation.
        /// </summary>
        /// <param name="identification">The login identifier (username or email) used in this transaction.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A redirect.</returns>
        public IActionResult OnPost(String identification, String password)
        {
            // Ensure username or email exists in the database
            if (db.Users.Where(u => String.Equals(u.Email, identification)).Count() > 0 ||
                db.Users.Where(u => String.Equals(u.UserName, identification)).Count() > 0)
            {
                // Get user object
                User tempUser = db.Users.SingleOrDefault(u => String.Equals(u.Email, identification) || String.Equals(u.UserName, identification));

                // Ensure user exists
                if (tempUser != null)
                {
                    if (String.Equals(EncryptionDecryptionHelper.Decrypt(tempUser.Password), password))
                    {
                        // Log in user
                        tempUser.LoggedIn = true;
                        HttpContext.Session.SetString("_User", JsonConvert.SerializeObject(tempUser));

                        return Redirect("~/MyListings");
                    } else
                    {
                        // Password did not match database
                        warnings.Add("Your password was incorrect. Please try again.");
                    }
                } else
                {
                    warnings.Add("System error occurred. Please try again later.");
                }
            }
            else
            {
                warnings.Add("There is no account with this username or email.");
            }

            return null;
        }
    }
}