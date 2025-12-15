using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PharmaGO.Application.Common.Auth.Constants;
using PharmaGO.Application.Common.Interfaces;

namespace PharmaGO.Infrastructure.Services;

public class PermissionService(
    UserManager<IdentityUser<Guid>> userManager,
    RoleManager<IdentityRole<Guid>> roleManager
) : IPermissionService
{
    public async Task<List<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return [];

        var userClaims = await userManager.GetClaimsAsync(user);
        var userPermissions = userClaims
            .Where(c => c.Type == CustomClaims.Permission)
            .Select(c => c.Value)
            .ToList();

        var userRoles = await userManager.GetRolesAsync(user);
        List<string> rolePermissions = [];

        foreach (var roleName in userRoles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null) continue;

            var roleClaims = await roleManager.GetClaimsAsync(role);
            var permissions = roleClaims
                .Where(c => c.Type == CustomClaims.Permission)
                .Select(c => c.Value);
            rolePermissions.AddRange(permissions);
        }

        return userPermissions.Concat(rolePermissions).Distinct().ToList();
    }

    public async Task<bool> UserHasPermissionAsync(
        Guid userId,
        string permission,
        CancellationToken cancellationToken = default
    )
    {
        var permissions = await GetUserPermissionsAsync(userId, cancellationToken);
        return permissions.Contains(permission);
    }

    public async Task<List<string>> GetRolePermissionsAsync(
        string roleName,
        CancellationToken cancellationToken = default
    )
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
            return [];

        var claims = await roleManager.GetClaimsAsync(role);
        return claims.Where(c => c.Type == CustomClaims.Permission).Select(c => c.Value).ToList();
    }

    public async Task AddPermissionToUserAsync(
        Guid userId,
        string permission,
        CancellationToken cancellationToken = default
    )
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new InvalidOperationException("User not found");

        var existingClaims = await userManager.GetClaimsAsync(user);
        var hasClaim = existingClaims.Any(c => c.Type == CustomClaims.Permission && c.Value == permission);

        if (!hasClaim)
        {
            await userManager.AddClaimAsync(user, new Claim(CustomClaims.Permission, permission));
        }
    }

    public async Task RemovePermissionFromUserAsync(
        Guid userId,
        string permission,
        CancellationToken cancellationToken = default
    )
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new InvalidOperationException("User not found");

        await userManager.RemoveClaimAsync(user, new Claim(CustomClaims.Permission, permission));
    }
}