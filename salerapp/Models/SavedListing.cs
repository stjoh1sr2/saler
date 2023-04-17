using Microsoft.Identity.Client;
using System.Drawing;

namespace salerapp.Models
{
    public class SavedListing
    {
        public int SavedListingId { get; set; }
        public int UserId { get; set; }
        public int ListingId { get; set; }
        public DateTime SavedTime { get; set; }
    }
}
