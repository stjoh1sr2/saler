using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;
using System.Collections.Immutable;

namespace salerapp.Pages
{
    public class SearchModel: PageModel
    {
        public SalerContext db = new SalerContext();
        public IEnumerable<Listing> listings;
        public Dictionary<int, String> userNames = new Dictionary<int, String>();
        private string[] commonWords = { "the", "a", "an", "and", "that"};

        public void LoadListings(string[] queryWords)
        {
            IEnumerable<Listing> tempListings = db.Listings.OrderByDescending(l => l.PostDate);
            Dictionary<int, int> matchCount = new Dictionary<int, int>();

            // Search algorithm
            foreach (Listing tempListing in tempListings)
            {
                matchCount.Add(tempListing.ListingId, 0);

                foreach (string queryWord in queryWords)
                {
                    // Filter out common words
                    if (!commonWords.Contains(queryWord.ToLower()) && tempListing.Description.ToLower().Contains(queryWord.ToLower()))
                    {
                        // Add to count
                        matchCount[tempListing.ListingId] = matchCount.GetValueOrDefault(tempListing.ListingId) + 1;
                    }
                }
            }

            // Add to listings enumerable, sorted by match count
            listings = tempListings.Where(t => matchCount.GetValueOrDefault(t.ListingId) > 0)
                .OrderByDescending(t => matchCount.GetValueOrDefault(t.ListingId));

            // Get usernames for display in search page
            foreach (Listing listing in db.Listings) {
                String userName = db.Users.Where(u => u.UserId == listing.PosterId).SingleOrDefault().UserName;
                userNames.Add(listing.ListingId, (userName is null) ? "Unknown" : userName);
            }
        }
    }
}