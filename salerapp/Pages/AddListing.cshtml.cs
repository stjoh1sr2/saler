using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    public class AddListingModel: PageModel
    {
        public Listing Listing { get; set; }
        public SalerContext db = new SalerContext();
        public IEnumerable<SelectListItem> StateOptions = Listing.StateAbbreviations
            .Select(x => new SelectListItem() { Text = x.ToString() });
        public List<String> warnings = new List<String>();

        public IActionResult OnPost(Listing listing)
        {
            if (listing != null)
            {
                if (!UserManagementHelper.GlobalUser.LoggedIn)
                {
                    warnings.Add("Please log in before submitting a listing.");
                    return null;
                } else
                {
                    listing.PosterId = UserManagementHelper.GlobalUser.UserId;
                    listing.PostDate = DateTime.Now;
                    db.Listings.Add(listing);
                    db.SaveChanges();
                    return Redirect("~/");
                }

            } else
            {
                warnings.Add("An error occured in processing your listing.");
                return Redirect("~/");
            }

            
        }
    }
}