namespace salerapp.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public int PosterId { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ShowDate { get; set; } 
        public DateTime HideDate { get; set; }
        public bool IsHidden { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public int ZIPCode { get; set; }
        public string? State { get; set; }
    }
}
