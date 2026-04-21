using AarhusSpaceProgram.Api.Entities;
using Microsoft.AspNetCore.Identity;

namespace AarhusSpaceProgram.Api.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        string[] roleNames = { "Astronaut", "Scientist", "Manager" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // 2. Seed Dummy Users
        await CreateUserAsync(userManager, "Ron@space.com", "Password1!", "Astronaut User", "Astronaut");
        await CreateUserAsync(userManager, "Harry@space.com", "Password1!", "Scientist User", "Scientist");

        // Note: We give the Manager all roles so they satisfy all explicit endpoint requirements 
        await CreateUserAsync(userManager, "Dumbledore@space.com", "Password1!", "Manager User", "Manager");
    }

    private static async Task CreateUserAsync(UserManager<AppUser> userManager, string email, string password, string fullName, params string[] roles)
    {
        if (await userManager.FindByNameAsync(email) == null)
        {
            var user = new AppUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                // Add the user to the assigned roles
                foreach (var role in roles)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}