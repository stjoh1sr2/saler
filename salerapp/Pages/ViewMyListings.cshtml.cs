using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            if (UserManagementHelper.GlobalUser.LoggedIn)
            {
                listings = db.Listings.Where(l => l.PosterId == UserManagementHelper.GlobalUser.UserId)
                    .OrderByDescending(l => l.EndDate);
            }
        }
    }
}