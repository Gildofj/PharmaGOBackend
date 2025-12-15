namespace PharmaGO.Application.Common.Interfaces;

public interface IPermissionService
{
    Task<List<string>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> UserHasPermissionAsync(Guid userId, string permission, CancellationToken cancellationToken = default);
    Task<List<string>> GetRolePermissionsAsync(string roleName, CancellationToken cancellationToken = default);
    Task AddPermissionToUserAsync(Guid userId, string permission, CancellationToken cancellationToken = default);
    Task RemovePermissionFromUserAsync(Guid userId, string permission, CancellationToken cancellationToken = default);
}