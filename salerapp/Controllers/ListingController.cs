using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;

namespace salerapp.Controllers
{
    /// <summary>
    /// Handles operations to do with garage sale listings.
    /// </summary>
    public class ListingController : Controller
    {
        public SalerContext db = new SalerContext();

        /// <summary>
        /// Toggles a listing from hidden to unhidden or unhidden to hidden.
        /// </summary>
        /// <param name="listingId">The ID of the listing to be hidden or unhidden.</param>
        /// <returns>Redirect to current page.</returns>
        public IActionResult ToggleHideListing(int listingId)
        {
            // Ensure that a user is currently logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                // Retrieve the current listing
                Listing currentListing = db.Listings.First(l => l.ListingId == listingId);

                // Validate that the current user is the poster for this listing
                if (JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId == currentListing.PosterId)
                {
                    // Toggle hidden flag
                    currentListing.IsHidden = !currentListing.IsHidden;
                    db.SaveChanges();
                }

            }

            // Redirect to current page
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        /// <summary>
        /// Saves a listing in the Saler database for later viewing by a particular user.
        /// </summary>
        /// <param name="listingId">The ID of the listing to be saved by a user.</param>
        /// <returns>Rediret to current page.</returns>
        public IActionResult SaveListing(int listingId)
        {
            // Make sure a user is logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                // Get user Id from session
                int userId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("_User")).UserId;

                // Ensure that the user does not have the listing currently saved before proceeding
                if (db.SavedListings.Where(s => s.UserId == userId && s.ListingId == listingId).Count() <= 0)
                {
                    // Create new saved listing relationship
                    SavedListing tempSaveListing = new SavedListing
                    {
                        ListingId = listingId,
                        UserId = userId,
                        SavedTime = DateTime.Now
                    };

                    // Add new saved listing to database
                    db.SavedListings.Add(tempSaveListing);
                    db.SaveChanges();
                }
            }

            // Redirect to current page
            return Redirect(HttpContext.Request.Headers["Referer"].ToString());
        }

        /// <summary>
        /// Removes the relationship between a user and a listing that they had previously saved.
        /// </summary>
        /// <param name="listingId">The ID of the listing to be unsaved by a user.</param>
        /// <returns>Redirect to current page.</returns>
        public IActionResult UnsaveListing(int listingId)
        {
            // Make sure a user is logged in
            if (HttpContext.Session.GetString("_User") is not null)
            {
                // Get user Id from session
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

        //[HttpPost]
        //public IActionResult SavePicture(ListingModel)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        var fileName = Path.GetFileName(file.FileName);
        //        var blobContainer = new BlobContainerClient(Configuration.GetConnectionString("AzureStorageAccount"), "images");
        //        var blobClient = blobContainer.GetBlobClient(fileName);
        //        using (var stream = file.OpenReadStream())
        //        {
        //            await blobClient.UploadAsync(stream);
        //        }
        //    }

        //    return RedirectToPage("/AddListing");
        //}
    }
}
