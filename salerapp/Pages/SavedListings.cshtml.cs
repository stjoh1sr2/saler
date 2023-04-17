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
    public class SavedListingsModel: PageModel
    {
        public SalerContext db = new SalerContext();
        public IEnumerable<Listing> listings;

        public void LoadSavedListings()
        {
            if (HttpContext.Session.GetString("_User") is not null)
            {
                int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;
                List<int> savedIds = db.SavedListings.Where(l => l.UserId == userId)
                    .OrderByDescending(l => l.SavedTime).Select(l => l.ListingId).ToList();

                if (savedIds is not null)
                {
                    listings = db.Listings.Where(l => savedIds.Contains(l.ListingId));
                }
            }
        }
    }
}