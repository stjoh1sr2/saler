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

namespace salerapp.Pages
{
    public class RegisterModel : PageModel
    {
		public SalerContext db = new SalerContext(); 
        public User user { get; set; }
        public String validationEmail { get; set; }
        public String validationPassword { get; set; }
        public List<String> warnings = new List<String>();

        public IActionResult OnPost(User user, String validationEmail, String validationPassword)
        {
            bool emailValidated = String.Equals(user.Email, validationEmail);
            bool passValidated = String.Equals(user.Password, validationPassword);
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
                UserManagementHelper.GlobalUser = user;
                UserManagementHelper.GlobalUser.LoggedIn = true;

                // Redirect to home page
                return Redirect("~/");
            } else
            {
                return null;
            }
		}
    }
}