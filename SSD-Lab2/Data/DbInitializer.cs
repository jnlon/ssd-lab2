using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SSD_Lab2.Data;
using SSD_Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Lab2.Data
{
    public static class DbInitializer
    {
        static IConfiguration _config;
        static IWebHostEnvironment _env;

        public static async Task<int> SeedUsersAndRoles(IServiceProvider serviceProvider)
        {
            // create the database if it doesn't exist
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            _config = serviceProvider.GetRequiredService<IConfiguration>();

            // Check if roles already exist and exit if there are
            if (roleManager.Roles.Count() > 0)
                return 1;  // should log an error message here

            // Seed roles
            int result = await SeedRoles(roleManager);
            if (result != 0)
                return 2;  // should log an error message here

            // Check if users already exist and exit if there are
            if (userManager.Users.Count() > 0)
                return 3;  // should log an error message here

            // Seed users
            result = await SeedUsers(userManager);
            // Console.WriteLine("SeedUsers result returned: " + result.ToString());
            if (result != 0)
                return 4;  // should log an error message here

            return 0;
        }

        private static async Task<int> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Create Admin Role
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!result.Succeeded)
                return 1;  // should log an error message here

            // Create Member Role
            result = await roleManager.CreateAsync(new IdentityRole("Member"));
            if (!result.Succeeded)
                return 2;  // should log an error message here

            return 0;
        }

        private static string GetPassword()
        {
            // Console.WriteLine($"GetPassword(): env.IsDevelopment() = {_env.IsDevelopment()}");
            // Console.WriteLine($"GetPassword(): DEFAULT_SEED_PASSWORD = {_config.GetValue<string>("DEFAULT_SEED_PASSWORD")}");
            // foreach (var c in _config.AsEnumerable())
            //    Console.WriteLine("ENUMERATE:"  + c.Key + " = " + c.Value);

            if (_env.IsDevelopment())
                return "Password!1";
            else
                return _config.GetValue<string>("DEFAULT_SEED_PASSWORD");
        }

        private static async Task<int> SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Create Admin User
            var adminUser = new ApplicationUser
            {
                UserName = "the.admin@mohawkcollege.ca",
                Email = "the.admin@mohawkcollege.ca",
                FirstName = "The",
                LastName = "Admin",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(adminUser, GetPassword());
            if (!result.Succeeded)
            {
                // Console.WriteLine("SeedUsers()-1 Errors: " + string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}")));
                return 1;  // should log an error message here
            }

            // Assign user to Admin role
            result = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!result.Succeeded)
            {
                // Console.WriteLine("SeedUsers()-2 Errors: " + string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}")));
                return 2;  // should log an error message here
            }

            // Create Member User
            var memberUser = new ApplicationUser
            {
                UserName = "the.member@mohawkcollege.ca",
                Email = "the.member@mohawkcollege.ca",
                FirstName = "The",
                LastName = "Member",
                EmailConfirmed = true
            };
            result = await userManager.CreateAsync(memberUser, GetPassword());
            if (!result.Succeeded)
                return 3;  // should log an error message here

            // Assign user to Member role
            result = await userManager.AddToRoleAsync(memberUser, "Member");
            if (!result.Succeeded)
                return 4;  // should log an error message here

            return 0;
        }
    }
}
