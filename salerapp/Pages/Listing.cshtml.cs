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
    /// PageModel for the Listing page.
    /// </summary>
    public class ListingModel: PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// Retrieves the listing based on ID from URL query.
        /// </summary>
        /// <param name="id">The listing to be retrieved.</param>
        /// <returns>The desired detailed garage sale listing.</returns>
        public Listing LoadListing(int id)
        {
            return db.Listings.Where(l => l.ListingId == id).SingleOrDefault();
		}

        /// <summary>
        /// Gets the user name of the listing's poster based on user ID.
        /// </summary>
        /// <param name="id">The poster ID of the listing.</param>
        /// <returns>The user name of the listing's poster.</returns>
        public string LoadUserName(int id)
        {
            // Get user object from database
            User tempUser = db.Users.Where(u => u.UserId == id).SingleOrDefault();

            return (tempUser is not null) ? tempUser.UserName : "Unknown";
        }
    }
}