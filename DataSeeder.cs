using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public static class DataSeeder
{
    public static async Task SeedAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        string adminRoleName = "Admin";
        string adminEmail = "admin@example.com";
        string adminPassword = "DefaultPassword123!";

        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRoleName));
        }

        ApplicationUser adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, adminRoleName);
        }
    }
}