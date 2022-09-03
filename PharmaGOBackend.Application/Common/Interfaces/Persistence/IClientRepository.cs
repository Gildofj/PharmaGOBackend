using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Common.Interfaces.Persistence;
public interface IClientRepository
{
    Client Add(Client client);
    Client? GetClientByEmail(string email);
}
