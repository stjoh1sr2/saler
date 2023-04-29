using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace salerapp.Models
{
    /// <summary>
    /// A garage sale listing.
    /// </summary>
    public class Listing
    {
        /// <summary>
        /// The primary ID of a listing.
        /// </summary>
        public int ListingId { get; set; }

        /// <summary>
        /// The ID of the user who posted a listing.
        /// </summary>
        public int PosterId { get; set; }

        /// <summary>
        /// The description of the garage sale listing.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The date/time that the garage sale begins.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The date/time that the garage sale ends.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The original date/time of the listing's submission.
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// The date/time that a listing becomes visible to the public.
        /// </summary>
        public DateTime ShowDate { get; set; }

        /// <summary>
        /// The date/time that a listing becomes hidden to the public.
        /// </summary>
        public DateTime HideDate { get; set; }

        /// <summary>
        /// Flags if the user has chosen to manually hide their listing.
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// The street address of the sale.
        /// </summary>
        public string? StreetAddress { get; set; }

        /// <summary>
        /// The city of the sale.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// The ZIP Code of the sale.
        /// </summary>
        public int ZIPCode { get; set; }

        /// <summary>
        /// The state of the sale.
        /// </summary>
        public string? State { get; set; }

        //[DisplayName("Upload Image")]
        //public string? FileDetails { get; set; }

        //[NotMapped]
        //public IFormFile? File { get; set; }

        /// <summary>
        /// A list of state abbreviations in alphabetical order of full state name.
        /// </summary>
        public static IEnumerable<String> StateAbbreviations = new List<String> {
            "AL",
            "AK",
            "AZ",
            "AR",
            "CA",
            "CO", 
            "CT",
            "DE",
            "DC",
            "FL",
            "GA",
            "HI",
            "ID",
            "IL",
            "IN",
            "IA",
            "KS",
            "KY",
            "LA",
            "ME",
            "MT",
            "NE",
            "NV",
            "NH",
            "NJ",
            "NM",
            "NY",
            "NC",
            "ND",
            "OH",
            "OK",
            "OR",
            "MD",
            "MA",
            "MI",
            "MN",
            "MS",
            "MO",
            "PA",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VT",
            "VA",
            "WA",
            "WV",
            "WI",
            "WY"};

        /// <summary>
        /// A list of state names in alphabetical order.
        /// </summary>
        public static IEnumerable<String> StateNames = new List<String> {
            "Alabama",
            "Alaska",
            "Arizona",
            "Arkansas",
            "California",
            "Colorado",
            "Connecticut",
            "Delaware",
            "District of Columbia",
            "Florida",
            "Georgia",
            "Hawaii",
            "Idaho",
            "Illinois",
            "Indiana",
            "Iowa",
            "Kansas",
            "Kentucky",
            "Louisiana",
            "Maine",
            "Montana",
            "Nebraska",
            "Nevada",
            "New Hampshire",
            "New Jersey",
            "New Mexico",
            "New York",
            "North Carolina",
            "North Dakota",
            "Ohio",
            "Oklahoma",
            "Oregon",
            "Maryland",
            "Massachusetts",
            "Michigan",
            "Minnesota",
            "Mississippi",
            "Missouri",
            "Pennsylvania",
            "Rhode Island",
            "South Carolina",
            "South Dakota",
            "Tennessee",
            "Texas",
            "Utah",
            "Vermont",
            "Virginia",
            "Washington",
            "West Virginia",
            "Wisconsin",
            "Wyoming"};
    }
}
