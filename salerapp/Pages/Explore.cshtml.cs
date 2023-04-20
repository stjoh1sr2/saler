using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    /// <summary>
    /// PageModel for the Explore page.
    /// </summary>
    public class ExploreModel: PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// The list of listings to be viewed.
        /// </summary>
        public IEnumerable<Listing> listings;

        /// <summary>
        /// A dictionary linking listing ID to the poster ID.
        /// </summary>
        public Dictionary<int, String> userNames = new Dictionary<int, String>();

        /// <summary>
        /// Retrieves the listings to be displayed.
        /// </summary>
        public void LoadListings()
        {
            // Sort listings
            listings = db.Listings.OrderByDescending(l => l.PostDate);

            // Get usernames for display in explore page
            foreach (Listing listing in db.Listings) {
                String userName = db.Users.Where(u => u.UserId == listing.PosterId).SingleOrDefault().UserName;
                userNames.Add(listing.ListingId, (userName is null) ? "Unknown" : userName);
            }
        }
    }
}