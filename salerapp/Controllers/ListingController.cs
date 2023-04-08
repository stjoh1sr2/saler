using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Controllers
{
    public class ListingController : Controller
    {
        public SalerContext db = new SalerContext();

        public IActionResult ToggleHideListing(int listingId)
        {
            if (HttpContext.Session.GetString("_User") is not null)
            {
                Listing currentListing = db.Listings.First(l => l.ListingId == listingId);

                // Validate that the current user is the poster for this listing
                if (JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId == currentListing.PosterId)
                {
                    // Toggle hidden flag
                    currentListing.IsHidden = !currentListing.IsHidden;
                    db.SaveChanges();
                }

            }

            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
    }
}
