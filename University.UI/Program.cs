using Microsoft.EntityFrameworkCore;
using University.Domain.Repositories;
using University.Infrastructure;
using University.Infrastructure.Repositories;
using University.Services;
using University.Services.Abstractions;
using University.UI.Controllers;
using University.UI.Middleware;

namespace University.UI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            MapsterConfiguration.Configure();

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
