using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Infrastructure.Persistence
{
    public class ClientRepository : IClientRepository
    {
        private static readonly List<Client> _clients = new();
        public void Add(Client client)
        {
            _clients.Add(client);
        }

        public Client? GetClientByEmail(string email)
        {
            return _clients.SingleOrDefault(u => u.Email == email);
        }
    }
}
