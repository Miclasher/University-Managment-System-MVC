using Microsoft.EntityFrameworkCore;
using University.Domain.Repositories;
using University.Infrastructure;
using University.Infrastructure.Repositories;
using University.Services;
using University.Services.Abstractions;
using University.UI.Controllers;
using University.UI.Middleware;
using WebOptimizer;

namespace University.UI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IViewDataService, ViewDataService>();

            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            var config = new ConfigurationBuilder()
                .AddUserSecrets<HomeController>().Build();

            builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(config["DbConnectionString"]));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddWebOptimizer(pipeline =>
            {
                pipeline.AddCssBundle("/css/bundle.css",
                    "css/*.css",
                    "lib/bootstrap/dist/css/bootstrap.min.css",
                    "/University.UI.styles.css");
                pipeline.AddJavaScriptBundle("/js/bundle.js",
                    "lib/jquery/dist/jquery.min.js",
                    "lib/bootstrap/dist/js/bootstrap.bundle.min.js",
                    "lib/jquery-validation/dist/jquery.validate.min.js",
                    "lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js",
                    "js/**/*.js");
            });

            var app = builder.Build();

            app.UseWebOptimizer();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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
