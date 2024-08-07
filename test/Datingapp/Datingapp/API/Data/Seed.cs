using Datingapp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Datingapp.API.Data
{
    public class Seed
    {
        //    public static async Task SeedUsers(UserManager<AppUser> userManager,
        //        RoleManager<AppRole> roleManager)
        //    {
        //        if (await userManager.Users.AnyAsync()) return;

        //        var userData = await System.IO.File.ReadAllTextAsync("API/Data/UserSeedData.json");
        //        var users= JsonSerializer.Deserialize<List<AppUser>>(userData);
        //        if(users ==null) return;

        //        var roles = new List<AppRole>
        //        {
        //            new AppRole{Name="Member"},
        //            new AppRole{Name="Admin"},
        //            new AppRole{Name="Moderator"},
        //        };

        //        foreach (var role in roles)
        //        {
        //            await roleManager.CreateAsync(role);
        //        }


        //        foreach (var user in users)
        //        {
        //            //using var hmac = new HMACSHA512();
        //            user.UserName = user.UserName.ToLower();
        //            //user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("ashish123"));
        //            //user.PasswordSalt = hmac.Key;

        //            //context.Users.Add(user);

        //             await userManager.CreateAsync(user, "Ashish123");
        //            await userManager.AddToRoleAsync(user, "Member");
        //            //if (!result.Succeeded)
        //            //{
        //            //    // Log errors if any
        //            //    foreach (var error in result.Errors)
        //            //    {
        //            //        Console.WriteLine($"Error creating user {user.UserName}: {error.Description}");
        //            //    }
        //            //}
        //        }

        //            var admin = new AppUser {
        //            UserName="admin"
        //            };

        //                await userManager.CreateAsync(admin, "Admin");
        //                await userManager.AddToRoleAsync(admin, "Admin");


        //        //await context.SaveChangesAsync();
        //    }
        //}

        public static async Task SeedUsers(UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager)
        {
            // Check if there are any users already
            if (await userManager.Users.AnyAsync()) return;

            // Read user seed data from JSON file
            var userData = await System.IO.File.ReadAllTextAsync("API/Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            // Define and create roles
            var roles = new List<AppRole> { 
                //"Member", "Admin", "Moderator
                new AppRole{Name="Member"},
                    new AppRole{Name="Admin"},
                    new AppRole{Name="Moderator"},
            };

            foreach (var roleName in roles)
            {
                    await roleManager.CreateAsync(roleName);
            }

            // Create and assign roles to users
            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                var result = await userManager.CreateAsync(user, "Ashish123"); // Use a strong password
                if (result.Succeeded)
                {
                    // Assign a single default role
                    var roleName = "Member"; // Change this to any single role you want to assign
                    var addToRoleResult = await userManager.AddToRoleAsync(user, roleName);
                }

            }

            // Create and assign roles to admin user
            var admin = new AppUser
            {
                UserName = "admin",
               
            };

            var adminResult = await userManager.CreateAsync(admin, "Admin123!"); // Use a strong password
            if (adminResult.Succeeded)
            {
                // Assign multiple roles to the admin user
                var adminRoles = new[] { "Admin", "Moderator" };

                foreach (var roleName in adminRoles)
                {
                  await userManager.AddToRoleAsync(admin, roleName);
                }
            }
        }
    }
    }
