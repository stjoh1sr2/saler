using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Pages
{
    public class ListingModel: PageModel
    {
        public SalerContext db = new SalerContext();

        public Listing LoadListing(int id)
        {
            return db.Listings.Where(l => l.ListingId == id).SingleOrDefault();
		}

        public string LoadUserName(int id)
        {
            User tempUser = db.Users.Where(u => u.UserId == id).SingleOrDefault();

            return (tempUser is not null) ? tempUser.UserName : "Unknown";
        }
    }
}