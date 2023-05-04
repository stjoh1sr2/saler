using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    /// <summary>
    /// PageModel for Add Listing page.
    /// </summary>
    public class AccountSettingsModel : PageModel
    {

        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// Warnings to display to the user.
        /// </summary>
        public List<String> warnings = new List<String>();

        public User LoadUser(int id)
        {
            return db.Users.Where(u => u.UserId == id).FirstOrDefault();
        }

        /// <summary>
        /// The user's password.
        /// </summary>
        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }


        public IActionResult OnPostUsername(User user)
        {

            int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;
            User currentUser = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
            bool newUserName = db.Users.Where(u => String.Equals(u.UserName.ToLower(), user.UserName.ToLower())).Count() == 0;
            if (!newUserName)
            {
                // Validate that there are no existing users with the username
                warnings.Add("That username already exists. Please choose another username.");
                return Page();
            }
            else
            {
                currentUser.UserName = user.UserName;
                db.SaveChanges();
                // update username in session
                string userJson = HttpContext.Session.GetString("_User");
                User sessionUser = JsonConvert.DeserializeObject<User>(userJson);
                sessionUser.UserName = user.UserName;
                HttpContext.Session.SetString("_User", JsonConvert.SerializeObject(sessionUser));
                // return success message and reload the page
                TempData["Message"] = "Username updated successfully.";
                return Page();
            }
        }

        public IActionResult OnPostPassword()
        {
            string currentPassword = CurrentPassword;
            string newPassword = NewPassword;

            // Get user object
            int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;
            User currentUser = db.Users.Where(u => u.UserId == userId).FirstOrDefault();

            // Ensure user exists
            if (currentUser != null)
            {
                if (String.Equals(currentPassword, newPassword))
                {
                    warnings.Add("Your new password cannot match your current password.");
                }
                else if (String.Equals(EncryptionDecryptionHelper.Decrypt(currentUser.Password), currentPassword))
                {
                    // Encrypt password before saving
                    currentUser.Password = EncryptionDecryptionHelper.Encrypt(newPassword);
                    db.SaveChanges();
                    // return success message and reload the page
                    TempData["Message"] = "Password updated successfully.";
                }
                else
                {
                    // Password did not match database
                    warnings.Add("Your password was incorrect. Please try again.");
                }
            }
            else
            {
                warnings.Add("System error occurred. Please try again later.");
            }
            return Page();
        }

        public IActionResult OnPostDeleteAccount()
        {
            return Redirect("DeleteAccount");
        }

    }
}