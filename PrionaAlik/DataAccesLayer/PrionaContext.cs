using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PrionaAlik.Controllers;
using PrionaAlik.Models;

namespace PrionaAlik.DataAccesLayer
{
	public class PrionaContext : IdentityDbContext
	{
        public PrionaContext(DbContextOptions<PrionaContext> options) : base(options) {

        }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<ProductCategory> ProductCategories { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach(var  entry in ChangeTracker.Entries())
			{
				switch (entry.State){
					case EntityState.Added:
						((BaseEntity)entry.Entity).CreatedTime = DateTime.Now;
						((BaseEntity)entry.Entity).IsDeleted = false;
						break;
						case EntityState.Modified:
						((BaseEntity)entry.Entity).UpdatedTime = DateTime.Now;
						break;
				}
			}  
			return base.SaveChangesAsync(cancellationToken);
		}
		//protected override void OnConfiguring(DbContextOptionsBuilder options)
		//{
		//	options.UseSqlServer(@"Server=DESKTOP-MPV3150;Database=PrionaAlik;Trusted_Connection=True;TrustServerCertificate=True");
		//		base.OnConfiguring(options);
		//}

	}
}
