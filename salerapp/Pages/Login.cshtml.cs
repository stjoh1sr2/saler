using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    public class LoginModel : PageModel
    {
        public SalerContext db = new SalerContext();
        public String identification { get; set; }
        public String password { get; set; }
        public List<String> warnings = new List<String>();

        public IActionResult OnPost(String identification, String password)
        {
            // Get info
            if (db.Users.Where(u => String.Equals(u.Email, identification)).Count() > 0 ||
                db.Users.Where(u => String.Equals(u.UserName, identification)).Count() > 0)
            {
                User tempUser = db.Users.SingleOrDefault(u => String.Equals(u.Email, identification) || String.Equals(u.UserName, identification));

                if (tempUser != null)
                {
                    if (String.Equals(EncryptionDecryptionHelper.Decrypt(tempUser.Password), password))
                    {
                        // Log in user
                        tempUser.LoggedIn = true;
                        HttpContext.Session.SetString("_User", JsonConvert.SerializeObject(tempUser));

                        return Redirect("~/ViewMyListings");
                    } else
                    {
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