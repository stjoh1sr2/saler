using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using salerapp.Models;
using salerapp.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using salerapp.Helpers;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;
using salerapp.Controllers;
using Newtonsoft.Json;

namespace salerapp.Pages
{
    /// <summary>
    /// PageModel for the Registration page.
    /// </summary>
    public class RegisterModel : PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
		public SalerContext db = new SalerContext(); 

        /// <summary>
        /// The new user object.
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// The email used to validate the original email field in registration.
        /// </summary>
        public String validationEmail { get; set; }

        /// <summary>
        /// The password used to validate the original email field in registration.
        /// </summary>
        public String validationPassword { get; set; }

        /// <summary>
        /// Warnings for the user.
        /// </summary>
        public List<String> warnings = new List<String>();

        /// <summary>
        /// Submits a new user for registration.
        /// </summary>
        /// <param name="user">The user to be registered.</param>
        /// <param name="validationEmail">The validation email used in the form.</param>
        /// <param name="validationPassword">The validation password used in the form.</param>
        /// <returns>A redirect.</returns>
        public IActionResult OnPost(User user, String validationEmail, String validationPassword)
        {
            // Ensure email and password match validation email and password
            bool emailValidated = String.Equals(user.Email, validationEmail);
            bool passValidated = String.Equals(user.Password, validationPassword);

            // Ensure username and email have not been taken
            bool newUserName = db.Users.Where(u => String.Equals(u.UserName.ToLower(), user.UserName.ToLower())).Count() == 0;
            bool newEmail = db.Users.Where(u => String.Equals(u.Email.ToLower(), user.Email.ToLower())).Count() == 0;

			if (!emailValidated)
            {
                // Ensure emails match
                warnings.Add("Make sure that your email addresses match.");
            }
			if (!passValidated)
			{
                // Ensure passwords match
				warnings.Add("Make sure that your passwords match.");
			}
            if (!newUserName)
            {
                // Validate that there are no existing users with the username
                warnings.Add("Your username is taken. Please choose another username.");
			}
            if (!newEmail)
            {
				// Validate that there are no existing users with the email
				warnings.Add("Your email address is taken. Try logging in on the login page or choose another address.");
			}
			
            if (emailValidated && passValidated && newUserName && newEmail)
			{
                // Encrypt password before saving
                user.Password = EncryptionDecryptionHelper.Encrypt(user.Password);

				db.Users.Add(user);
                db.SaveChanges();

                // Set user as logged in user
                user.LoggedIn = true;
                HttpContext.Session.SetString("_User", JsonConvert.SerializeObject(user));

                // Redirect to home page
                return Redirect("~/");
            } else
            {
                return null;
            }
		}
    }
}