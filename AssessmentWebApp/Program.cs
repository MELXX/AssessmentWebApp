using AssessmentWebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace AssessmentWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");//Environment.GetEnvironmentVariable("SQL_SERVER_CONNSTR");
            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString).EnableSensitiveDataLogging());
            

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using ( var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<AppDbContext>().Database;
                if (db.GetPendingMigrations().Count() > 0)
                {
                    db.Migrate();
                }
            }
            
            app.Run();
        }
    }
}