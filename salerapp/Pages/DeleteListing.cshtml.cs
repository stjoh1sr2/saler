using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Models;
using System.Reflection;

namespace salerapp.Pages
{
    public class DeleteListingModel : PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();
        /// <summary>
        /// Retrieves the details of the original listing.
        /// </summary>
        /// <param name="id">The listing ID of the original listing.</param>
        /// <returns>The desired listing and its details.</returns>
        public Listing LoadListing(int id)
        {
            return db.Listings.Where(l => l.ListingId == id).FirstOrDefault();
        }

        public IActionResult OnPost(Listing listing)
        {
            // Get the current listing based on ID
            Listing currentListing = db.Listings.Where(l => l.ListingId == listing.ListingId).FirstOrDefault();

            if (currentListing is null || HttpContext.Session.GetString("_User") is null)
            {
                // Redirect if listing not found or no user logged in
                return Redirect("~/");
            }
            else if (JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId != currentListing.PosterId)
            {
                // Redirect if logged in user does not match original listing poster
                return Redirect("~/");
            }
            else
            {

                // Remove listing information that could've changed
                db.Listings.Remove(currentListing);
                // Remove saved listings relationships
                var savedListings = db.SavedListings.Where(l => l.ListingId == currentListing.ListingId).ToList();
                foreach (SavedListing sl in savedListings)
                {
                    db.SavedListings.Remove(sl);
                }
                TempData["Message"] = "Listing Deleted Successfully";
                TempData.Keep();

                db.SaveChanges();
                return Redirect("~/MyListings");
            }
        }
    }
}
