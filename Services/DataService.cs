using BlogProject.Data;
using BlogProject.Enums;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            await _dbContext.Database.MigrateAsync();

            await SeedRolesAsync();

            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            //if roles are in the system already do nothing
            if (_dbContext.Roles.Any()) return;

            //otherwise we create roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            // same as in SeedRolesAsync
            if (_dbContext.Users.Any()) return;

            var adminUser = new BlogUser()
            {
                Email = "czerwonogrodzkipatryk@gmail.com",
                UserName = "czerwonogrodzkipatryk@gmail.com",
                FirstName = "Patryk",
                LastName = "Czerwonogrodzki",
                DisplayName = "Patryk Czerwonogrodzki",
                PhoneNumber = "123456789",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(adminUser, "Abc123@!");

            await _userManager.AddToRoleAsync(adminUser, BlogRole.Admin.ToString());

            var modUser = new BlogUser()
            {
                Email = "czerwonogrodzkipatryk@mailinator.com",
                UserName = "czerwonogrodzkipatryk@mailinator.com",
                FirstName = "Patryk1",
                LastName = "Czerwonogrodzki1",
                DisplayName = "Patryk Czerwonogrodzki1",
                PhoneNumber = "1234567890",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(modUser, "Abc123@!1");

            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());

        }
    }
}
