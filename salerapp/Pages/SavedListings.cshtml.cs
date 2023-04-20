using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;
using System.Collections;

namespace salerapp.Pages
{
    /// <summary>
    /// PageModel for the Saved Listings page.
    /// </summary>
    public class SavedListingsModel: PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// Listings that have been saved by the user.
        /// </summary>
        public IEnumerable<Listing> listings;

        /// <summary>
        /// Retrieves the listings that a user has saved.
        /// </summary>
        public void LoadSavedListings()
        {
            // Ensure user is logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;

                // Get listing IDs of saved listings for this user
                List<int> savedIds = db.SavedListings.Where(l => l.UserId == userId)
                    .OrderByDescending(l => l.SavedTime).Select(l => l.ListingId).ToList();

                // Send saved listings to page view
                if (savedIds is not null)
                {
                    listings = db.Listings.Where(l => savedIds.Contains(l.ListingId));
                }
            }
        }
    }
}