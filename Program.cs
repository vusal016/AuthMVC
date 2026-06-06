using AuthAdminCrud.MVC.Data.Seed;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AuthAdminCrud.MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            }).AddEntityFrameworkStores<AuthDbContext>();
            //Movcud cookie ni konfiqurasiya etmek ucun ConfigureApplicationCookie metodundan istifade edirik
            builder.Services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });
            //ve ya builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie() ile de konfiqurasiya etmek olar
            //Authentication yoxlanisi eger user login olmayibsa onu login sehifesine atsin
            //ve ya Admin paneline girmeye calisirsansa onu login sehifesine atsin
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //            {
            //                options.LoginPath = "/Account/Login";
            //                options.AccessDeniedPath = "/Account/AccessDenied";
            //            });

            var app = builder.Build();
            await SeederProgram.SeedData(app.Services);
            //Duzgun middleware sirasi:
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //Area routing default routingden evvel olmalidir.
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}