using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Core.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Client client);
}
