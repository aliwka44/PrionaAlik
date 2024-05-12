using Microsoft.EntityFrameworkCore;
using PrionaAlik.Models;

namespace PrionaAlik.DataAccesLayer
{
	public class PrionaContext : DbContext
	{
        public PrionaContext(DbContextOptions options) : base(options) {

        }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			options.UseSqlServer(@"Server=DESKTOP-MPV3150;Database=PrionaAlik;Trusted_Connection=True;TrustServerCertificate=True");
				base.OnConfiguring(options);
		}

	}
}
