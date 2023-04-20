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
    /// PageModel for the Edit Listing page.
    /// </summary>
    public class EditListingModel: PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// Warnings for the user.
        /// </summary>
        public IEnumerable<string> warnings = new List<string>();

        /// <summary>
        /// State abbreviation options for dropdown state selection.
        /// </summary>
        public IEnumerable<SelectListItem> StateOptions = Listing.StateAbbreviations
            .Select(x => new SelectListItem() { Text = x.ToString() });

        /// <summary>
        /// Retrieves the details of the original listing.
        /// </summary>
        /// <param name="id">The listing ID of the original listing.</param>
        /// <returns>The desired listing and its details.</returns>
        public Listing LoadListing(int id)
        {
            return db.Listings.Where(l => l.ListingId == id).FirstOrDefault();
        }

        /// <summary>
        /// Submits the edited listing to the Saler database.
        /// </summary>
        /// <param name="listing">The listing with changes.</param>
        /// <returns>A Redirect.</returns>
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
                // Update listing information that could've changed
                currentListing.StartDate = listing.StartDate;
                currentListing.EndDate = listing.EndDate;
                currentListing.Description = listing.Description;
                currentListing.StreetAddress = listing.StreetAddress;
                currentListing.City = listing.City;
                currentListing.State = listing.State;
                currentListing.ZIPCode = listing.ZIPCode;

                db.SaveChanges();
                return Redirect("~/listing?id=" + currentListing.ListingId);
            }
            
        }
    }
}