using LTKGMaster.Models;
using LTKGMaster.Models.Products;
using LTKGMaster.Models.Repositories;
using LTKGMaster.Models.SalesAd;
using LTKGMaster.Models.Users;

namespace LTKGMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Dependency injection for repositories
            builder.Services.AddSingleton<IAccountRepository, AccountRepository>();
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddSingleton<ISalesAdRepository, SalesAdRepository>();
            builder.Services.AddSingleton<IPictureRepository, PictureRepository>();
            builder.Services.AddSingleton<ProductFactory>();
            builder.Services.AddSingleton<SalesAdHandler>();
           

            // Dependency injection for HTTPContextAccessor used in login methods
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<ILogin, LoginWithAuthentication>();

            //Cookie authentication service is added
            builder.Services.AddAuthentication(CookieConstants.CookieName).AddCookie(CookieConstants.CookieName, options =>
            {
                options.Cookie.Name = CookieConstants.CookieName;
                options.LoginPath = "/Account/Login";
                options.ExpireTimeSpan = TimeSpan.FromDays(90);
                //options.AccessDeniedPath = "/Account/AccessDenied";
            });

            //Default and custom authorizations are added here
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
            }
            );


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
