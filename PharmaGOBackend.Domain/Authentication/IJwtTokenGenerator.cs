using PharmaGOBackend.Core.Entities;

namespace PharmaGOBackend.Core.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Client client);
}
