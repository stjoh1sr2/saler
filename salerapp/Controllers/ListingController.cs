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

        public IActionResult SaveListing(int listingId)
        {
            // Make sure a user is logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;

                // Only add new saved listings
                if (db.SavedListings.Where(s => s.UserId == userId && s.ListingId == listingId).Count() <= 0)
                {
                    SavedListing tempSaveListing = new SavedListing
                    {
                        ListingId = listingId,
                        UserId = userId,
                        SavedTime = DateTime.Now
                    };

                    // Add new listing to database
                    db.SavedListings.Add(tempSaveListing);
                    db.SaveChanges();
                }
            }

            // Redirect to current page
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        public IActionResult UnsaveListing(int listingId)
        {
            // Make sure a user is logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;

                // Make sure currently saved user has this listing saved
                if (db.SavedListings.Where(s => s.UserId == userId && s.ListingId == listingId).Count() >= 1)
                {
                    // Remove listing
                    db.SavedListings.Remove(db.SavedListings.Where(s => s.UserId == userId && s.ListingId == listingId).FirstOrDefault());
                    db.SaveChanges();
                }
            }

            // Redirect to current page
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }
    }
}
