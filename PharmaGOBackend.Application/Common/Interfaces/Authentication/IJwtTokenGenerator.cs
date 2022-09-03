using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Common.Interfaces.Authentication;
public interface IJwtTokenGenerator
{
    string GenerateToken(Client client);
}
