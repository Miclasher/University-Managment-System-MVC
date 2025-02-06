using Microsoft.EntityFrameworkCore;
using University.DataLayer;

namespace University
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var config = new ConfigurationBuilder()
                            .AddUserSecrets<UniversityContext>()
                            .Build();

            builder.Services.AddDbContext<UniversityContext>(options => options.UseSqlServer(config["DbConnectionString"]));

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
