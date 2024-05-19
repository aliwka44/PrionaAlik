using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PrionaAlik.Controllers;
using PrionaAlik.DataAccesLayer;

namespace PrionaAlik
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PrionaContext>(options => options.UseSqlServer
            (builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit=false;
                opt.Password.RequireLowercase=false;
                opt.Password.RequireUppercase=false;

            });
            var app = builder.Build();
            
            app.UseStaticFiles();
			app.MapControllerRoute("areas","{area:exists}/{controller=Slider}/{action=Index}/{id?}"
		  );
			app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
