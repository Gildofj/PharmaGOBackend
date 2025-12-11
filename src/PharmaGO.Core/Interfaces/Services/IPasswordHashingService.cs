using PharmaGO.Core.Entities;

namespace PharmaGO.Core.Interfaces.Services;

public interface IPasswordHashingService
{
    public string HashPassword(User user, string password);
    public bool VerifyPasswordHash(User user, string password, string hashedPassword);
}