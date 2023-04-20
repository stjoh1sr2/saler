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
    /// PageModel for the MyListings page.
    /// </summary>
    public class MyListingsModel: PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// The logged-in user's listings.
        /// </summary>
        public IEnumerable<Listing> listings;

        /// <summary>
        /// Retrieves listings for the logged-in user.
        /// </summary>
        public void LoadListings()
        {
            // Ensure user is logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;

                // Get user's listings and save to model
                listings = db.Listings.Where(l => l.PosterId == userId)
                    .OrderByDescending(l => l.EndDate);
            }
        }
    }
}