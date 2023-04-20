using Microsoft.EntityFrameworkCore;
using salerapp.Models;

namespace salerapp.Context
{
	/// <summary>
	/// Instantiates connections to the Saler database.
	/// </summary>
    public class SalerContext: DbContext
    {
		/// <summary>
		/// Users for the Saler application.
		/// </summary>
        public virtual DbSet<User> Users { get; set; }

		/// <summary>
		/// Listings for the Saler applications.
		/// </summary>
        public virtual DbSet<Listing> Listings { get; set; }

		/// <summary>
		/// Mapped pairs of user IDs and listing IDs for saved IDs in Saler.
		/// </summary>
		public virtual DbSet<SavedListing> SavedListings { get; set; }

		/// <summary>
		/// Instantiates connections to the Saler database.
		/// </summary>
		public SalerContext() { }

        /// <summary>
        /// Instantiates connections to the Saler database.
        /// </summary>
        /// <param name="options">Configuration options to be passed in the database connection.</param>
        public SalerContext(DbContextOptions<SalerContext> options): base(options) { }

		/// <summary>
		/// Handles initial configuring for the Saler database.
		/// </summary>
		/// <param name="optionsBuilder">A database configuration builder.</param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Build configuration from appsettings.json
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("SalerContext");
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}
