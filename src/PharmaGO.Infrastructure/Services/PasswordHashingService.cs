using Microsoft.AspNetCore.Identity;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Services;

namespace PharmaGO.Infrastructure.Services;

public class PasswordHashingService : IPasswordHashingService
{
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPasswordHash(User user, string password, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            user,
            hashedPassword,
            password
        );

        return result == PasswordVerificationResult.Success;
    }
}