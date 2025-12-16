using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Core.Entities;
using PharmaGO.Core.ValueObjects;

namespace PharmaGO.Infrastructure.Persistence.Seed;

public static class PharmacyDemoSeed
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<PharmaGOContext>();
        var config = services.GetRequiredService<IConfiguration>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser<Guid>>>();

        var pharmacyId = Guid.NewGuid();

        if (!context.Pharmacies.Any(p => p.Cnpj == "12.345.678/0001-99"))
        {
            var address = Address.Create(
                street: "Rodovia Francisco Thomaz dos Santos",
                number: "1234",
                neighborhood: "Armação do Pantano do Sul",
                city: "Florianópolis",
                state: "SC",
                country: "Brasil",
                zipCode: "88066000"
            );

            var pharmacy = new Pharmacy
            {
                Id = pharmacyId,
                Name = "PharmaGO Demo",
                Cnpj = "12.345.678/0001-99",
                ContactNumber = "12345678901",
                Address = address.Value,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            context.Pharmacies.Add(pharmacy);
            await context.SaveChangesAsync();
        }

        var adminEmail = config["AdminUser:Email"];
        var adminPassword = config["AdminUser:Password"];

        if (adminEmail == null || adminPassword == null)
        {
            throw new Exception("AdminUser Email or Password is missing");
        }

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser<Guid>
            {
                Id = Guid.NewGuid(),
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, EmployeeRoles.Admin);
            }
            else
            {
                throw new Exception(result.Errors.First().Description);
            }
        }

        if (!context.Employees.Any(e => e.Email == adminEmail))
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                FirstName = "Admin",
                LastName = "Demo",
                Email = adminEmail,
                Phone = "(99) 99999-9999",
                IdentityUserId = adminUser.Id,
                PharmacyId = pharmacyId,
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();
        }
    }
}