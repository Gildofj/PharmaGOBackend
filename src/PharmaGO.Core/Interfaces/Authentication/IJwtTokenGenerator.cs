using PharmaGO.Core.Entities;

namespace PharmaGO.Core.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Client client);
}
