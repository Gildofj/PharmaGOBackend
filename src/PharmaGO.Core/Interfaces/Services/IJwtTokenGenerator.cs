using PharmaGO.Core.Entities;

namespace PharmaGO.Core.Interfaces.Services;
public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
