using Microsoft.EntityFrameworkCore;
using salerapp.Models;

namespace salerapp.Context
{
    public class SalerContext: DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Listing> Listings { get; set; }

		public SalerContext() { }
		public SalerContext(DbContextOptions<SalerContext> options): base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("SalerContext");
			optionsBuilder.UseSqlServer(connectionString);
		}
	}
}
