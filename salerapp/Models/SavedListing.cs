using Microsoft.Identity.Client;
using System.Drawing;

namespace salerapp.Models
{
    /// <summary>
    /// Represents the relationship between a user and a listing through the save feature.
    /// </summary>
    public class SavedListing
    {
        /// <summary>
        /// The primary ID of the saved listing relationship.
        /// </summary>
        public int SavedListingId { get; set; }

        /// <summary>
        /// The User ID of the user who saved the listing.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The ID of the saved listing.
        /// </summary>
        public int ListingId { get; set; }

        /// <summary>
        /// The date/time the listing was originally saved.
        /// </summary>
        public DateTime SavedTime { get; set; }
    }
}
