
using CatBaBooking.Models;
using CatBaBooking.Repository;
using CatBaBooking.Service;
using Microsoft.EntityFrameworkCore;

namespace CatBaBooking;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<CatbabookingContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn")));

        builder.Services.Scan(scan => scan
                .FromAssemblyOf<LoginService>()
                .AddClasses(classes => classes
                    .Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<BusinessRepository>()
            .AddClasses(classes => classes
                .Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles(); 
        app.UseRouting();
        app.UseSession();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=dashboard}/{action=Index}/{id?}");

        app.Run();
    }
}