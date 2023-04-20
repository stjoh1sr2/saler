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
    /// PageModel for Add Listing page.
    /// </summary>
    public class AddListingModel: PageModel
    {
        /// <summary>
        /// The listing to be added.
        /// </summary>
        public Listing Listing { get; set; }

        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// A list of state abbreviations for the state dropdown menu.
        /// </summary>
        public IEnumerable<SelectListItem> StateOptions = Listing.StateAbbreviations
            .Select(x => new SelectListItem() { Text = x.ToString() });

        /// <summary>
        /// Warnings to display to the user.
        /// </summary>
        public List<String> warnings = new List<String>();

        /// <summary>
        /// Submits a listing to the database upon form submission.
        /// </summary>
        /// <param name="listing">The listing to be added.</param>
        /// <returns>A redirect to the page of the new listing, or home page if an error occurred.</returns>
        public IActionResult OnPost(Listing listing)
        {
            if (listing != null)
            {
                // Ensure logged in user is not null
                if (HttpContext.Session.GetString("_User") is null)
                {
                    warnings.Add("Please log in before submitting a listing.");
                    return null;
                } else
                {
                    // Get user ID from logged in user
                    listing.PosterId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;
                    listing.PostDate = DateTime.Now;

                    // Add listing
                    db.Listings.Add(listing);
                    db.SaveChanges();

                    // Redirect to listing page
                    return Redirect("~/listing?id=" + listing.ListingId);
                }

            } else
            {
                warnings.Add("An error occured in processing your listing.");
                return Redirect("~/");
            }
        }
    }
}