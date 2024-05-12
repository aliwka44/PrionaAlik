using PrionaAlik.DataAccesLayer;

namespace PrionaAlik
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<PrionaContext>(ServiceLifetime.Singleton);
            var app = builder.Build();
            app.UseStaticFiles();
			app.MapControllerRoute("areas","{area:exists}/{controller=Slider}/{action=Index}/{id?}"
		  );
			app.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
