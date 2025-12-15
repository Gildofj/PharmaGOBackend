using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Core.Common.Constants;

namespace PharmaGO.Infrastructure.Persistence.Seed;

public static class IdentitySeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser<Guid>>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var config = services.GetRequiredService<IConfiguration>();

        await SeedRolesAsync(roleManager);
        await SeedUsersAsync(userManager, config);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(nameof(UserType.MasterAdmin)))
        {
            var masterRole = new IdentityRole<Guid>(nameof(UserType.MasterAdmin));
            await roleManager.CreateAsync(masterRole);

            await roleManager.AddClaimAsync(masterRole, new Claim(CustomClaims.Permission, Permissions.CreatePharmacy));
            await roleManager.AddClaimAsync(masterRole, new Claim(CustomClaims.Permission, Permissions.UpdatePharmacy));
            await roleManager.AddClaimAsync(masterRole, new Claim(CustomClaims.Permission, Permissions.DeletePharmacy));
            await roleManager.AddClaimAsync(masterRole,
                new Claim(CustomClaims.Permission, Permissions.CreateMasterUsers));
        }

        if (!await roleManager.RoleExistsAsync(EmployeeRoles.Admin))
        {
            var adminRole = new IdentityRole<Guid>(EmployeeRoles.Admin);
            await roleManager.CreateAsync(adminRole);

            await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaims.Permission, Permissions.ManageProducts));
            await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaims.Permission, Permissions.ManageUsers));
            await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaims.Permission, Permissions.ManageOrders));
        }

        if (!await roleManager.RoleExistsAsync(EmployeeRoles.Employee))
        {
            var employeeRole = new IdentityRole<Guid>(EmployeeRoles.Employee);
            await roleManager.CreateAsync(employeeRole);

            await roleManager.AddClaimAsync(
                employeeRole,
                new Claim(CustomClaims.Permission, Permissions.ManageProducts)
            );
        }

        if (!await roleManager.RoleExistsAsync(nameof(UserType.Client)))
        {
            var clientRole = new IdentityRole<Guid>(nameof(UserType.Client));
            await roleManager.CreateAsync(clientRole);

            await roleManager.AddClaimAsync(clientRole, new Claim(CustomClaims.Permission, Permissions.ClientAccess));
        }
    }

    private static async Task SeedUsersAsync(UserManager<IdentityUser<Guid>> userManager, IConfiguration config)
    {
        var email = config["AdminUser:Email"];
        var password = config["AdminUser:Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new InvalidOperationException("Admin user email or password is missing");

        var masterUser = await userManager.FindByEmailAsync(email);
        if (masterUser == null)
        {
            masterUser = new IdentityUser<Guid>
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(masterUser, password);
            if (!result.Succeeded)
            {
                throw new Exception(
                    $"Failed to create admin user: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                );
            }
        }

        if (!await userManager.IsInRoleAsync(masterUser, nameof(UserType.MasterAdmin)))
        {
            await userManager.AddToRoleAsync(masterUser, nameof(UserType.MasterAdmin));
        }
    }
}