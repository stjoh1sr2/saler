using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    public class EditListingModel: PageModel
    {
        public SalerContext db = new SalerContext();
        public IEnumerable<string> warnings = new List<string>();
        public IEnumerable<SelectListItem> StateOptions = Listing.StateAbbreviations
            .Select(x => new SelectListItem() { Text = x.ToString() });

        public Listing LoadListing(int id)
        {
            return db.Listings.Where(l => l.ListingId == id).FirstOrDefault();
        }

        public IActionResult OnPost(Listing listing)
        {
            Listing currentListing = db.Listings.Where(l => l.ListingId == listing.ListingId).FirstOrDefault();

            if (currentListing is null || HttpContext.Session.GetString("_User") is null)
            {
                return Redirect("~/");
            } 
            else if (JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId != currentListing.PosterId)
            {
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