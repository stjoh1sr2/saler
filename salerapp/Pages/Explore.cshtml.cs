using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    public class ExploreModel: PageModel
    {
        public SalerContext db = new SalerContext();
        public IEnumerable<Listing> listings;
        public Dictionary<int, String> userNames = new Dictionary<int, String>();

        public void LoadListings()
        {
            listings = db.Listings.OrderByDescending(l => l.PostDate);

            // Get usernames for display in explore page
            foreach (Listing listing in db.Listings) {
                String userName = db.Users.Where(u => u.UserId == listing.PosterId).SingleOrDefault().UserName;
                userNames.Add(listing.ListingId, (userName is null) ? "Unknown" : userName);
            }
        }
    }
}