using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Models;
using System.Reflection;

namespace salerapp.Pages
{
    public class DeleteAccountModel : PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        public IActionResult OnPost()
        {
            int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;
            User currentUser = db.Users.Where(u => u.UserId == userId).FirstOrDefault();

            if (currentUser == null)
            {
                // Redirect if no user logged in
                return Redirect("~/");
            }

            // Remove all listings associated with the user
            List<Listing> listingsToRemove = db.Listings.Where(l => l.PosterId == userId).ToList();
            db.Listings.RemoveRange(listingsToRemove);

            // Remove all of the user's saved listings
            List<SavedListing> savedListings = db.SavedListings.Where(s => s.UserId == userId).ToList();
            db.SavedListings.RemoveRange(savedListings);

            // Remove the user's account
            db.Users.Remove(currentUser);

            // Save changes to the database
            db.SaveChanges();

            // Clear the session and redirect to the home page
            HttpContext.Session.Clear();
            TempData["Message"] = "Account deleted successfully.";
            return Redirect("~/");
        }
    }
}
