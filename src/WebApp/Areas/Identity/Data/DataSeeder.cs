using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

public static class DataSeeder
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Define roles first
        var roles = new string[] { "Admin", "Seller", "Manager" };

        // Add roles to RoleManager if they do not exist
        foreach (var role in roles)
        {
            var roleExist = await roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed Admin User
        var adminUser = await userManager.FindByEmailAsync("admin@example.com");
        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com"
            };

            var result = await userManager.CreateAsync(adminUser, "Password123!");

            if (result.Succeeded)
            {
                // Add the "Admin" role
                await userManager.AddToRoleAsync(adminUser, "Admin");

                // Add claim for Admin Policy
                await userManager.AddClaimAsync(adminUser, new Claim("Position", "Admin"));
            }
        }

        // Seed Seller User
        var sellerUser = await userManager.FindByEmailAsync("seller@example.com");
        if (sellerUser == null)
        {
            sellerUser = new IdentityUser
            {
                UserName = "seller@example.com",
                Email = "seller@example.com"
            };

            var result = await userManager.CreateAsync(sellerUser, "Password123!");

            if (result.Succeeded)
            {
                // Add the "Seller" role
                await userManager.AddToRoleAsync(sellerUser, "Seller");

                // Add claim for Seller Policy
                await userManager.AddClaimAsync(sellerUser, new Claim("Position", "Seller"));
            }
        }

        // Seed Manager User
        var managerUser = await userManager.FindByEmailAsync("manager@example.com");
        if (managerUser == null)
        {
            managerUser = new IdentityUser
            {
                UserName = "manager@example.com",
                Email = "manager@example.com"
            };

            var result = await userManager.CreateAsync(managerUser, "Password123!");

            if (result.Succeeded)
            {
                // Add the "Manager" role
                await userManager.AddToRoleAsync(managerUser, "Manager");

                // Add claim for Manager Policy
                await userManager.AddClaimAsync(managerUser, new Claim("Position", "Manager"));
            }
        }
    }
}
