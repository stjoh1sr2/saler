using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using salerapp.Context;
using salerapp.Helpers;
using salerapp.Models;
using System.Collections.Immutable;

namespace salerapp.Pages
{
    /// <summary>
    /// PageModel for the Search page.
    /// </summary>
    public class SearchModel: PageModel
    {
        /// <summary>
        /// The context linking the application to the Saler database.
        /// </summary>
        public SalerContext db = new SalerContext();

        /// <summary>
        /// Listings to display in search results.
        /// </summary>
        public IEnumerable<Listing> listings;

        /// <summary>
        /// Usernames for listings.
        /// </summary>
        public Dictionary<int, String> userNames = new Dictionary<int, String>();

        /// <summary>
        /// Common words to avoid in processing search results.
        /// </summary>
        private string[] commonWords = { "the", "a", "an", "and", "that"};

        /// <summary>
        /// Loads listings that included in the search results.
        /// </summary>
        /// <param name="queryWords">The list of search terms.</param>
        public void LoadListings(string[] queryWords)
        {
            // Get listings by post date
            IEnumerable<Listing> tempListings = db.Listings.OrderByDescending(l => l.PostDate);
            // Keeps track of the number of keyword hits per listing
            Dictionary<int, int> matchCount = new Dictionary<int, int>();

            // Loop through listings
            foreach (Listing tempListing in tempListings)
            {
                matchCount.Add(tempListing.ListingId, 0);

                // Loop through query keywords to match search results
                foreach (string queryWord in queryWords)
                {
                    // Filter out common words
                    if (!commonWords.Contains(queryWord.ToLower()) && tempListing.Description.ToLower().Contains(queryWord.ToLower()))
                    {
                        // Add to count if match was hit
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