using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    public class ViewMyListingModel: PageModel
    {
        public SalerContext db = new SalerContext();
        public IEnumerable<Listing> listings;

        public void LoadListings()
        {
            if (HttpContext.Session.GetString("_User") is not null)
            {
                int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;
                listings = db.Listings.Where(l => l.PosterId == userId)
                    .OrderByDescending(l => l.EndDate);
            }
        }
    }
}