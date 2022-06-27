using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Application.Common.Interfaces.Persistence
{
    public interface IClientRepository
    {
        Client? GetClientByEmail(string email);
        void Add(Client client);
    }
}
