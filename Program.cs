using BlogProject.Data;
using BlogProject.Models;
using BlogProject.Services;
using BlogProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<BlogUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            builder.Services.AddRazorPages();

            builder.Services.AddScoped<DataService>();

            builder.Services.AddScoped<IBlogEmailSender, EmailService>();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<ISlugService, SlugService>();
            builder.Services.AddScoped<BlogSearchService>();

            var app = builder.Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Services.CreateScope().ServiceProvider.GetRequiredService<DataService>().ManageDataAsync();

            app.UseHttpsRedirection();

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Index";
                    await next();
                }
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "slug",
                pattern: "post/{*slug}",
                defaults: new { controller = "Posts", action = "Details" });

            app.MapControllerRoute(
                name: "name",
                pattern: "blog/{*id}",
                defaults: new { controller = "Posts", action = "BlogPostIndex" });

            app.MapControllerRoute(
                name: "tag",
                pattern: "tag/{*id}",
                defaults: new { controller = "Posts", action = "TagIndex" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}