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
            IEnumerable<Listing> tempListings = db.Listings.Where(l => !l.IsHidden).OrderByDescending(l => l.PostDate);
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
                    if (!commonWords.Contains(queryWord.ToLower()))
                    {
                        bool descriptionHit = tempListing.Description.ToLower().Contains(queryWord.ToLower());
                        bool cityHit = tempListing.City.ToLower().Contains(queryWord.ToLower());
                        bool stateHit = tempListing.State.ToLower().Contains(queryWord.ToLower());
                        if (descriptionHit || stateHit)
                        {
                           // Add to count 1 if match was hit for description or state
                           matchCount[tempListing.ListingId] = matchCount.GetValueOrDefault(tempListing.ListingId) + 1;
                        }
                        if (cityHit)
                        {
                            // Add to count 3 if match was hit for city (to weigh city higher in search)
                            matchCount[tempListing.ListingId] = matchCount.GetValueOrDefault(tempListing.ListingId) + 3;
                        }
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