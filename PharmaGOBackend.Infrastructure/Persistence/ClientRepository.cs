using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Infrastructure.Persistence
{
    public class ClientRepository : IClientRepository
    {
        private readonly PharmaGOContext _db;

        public ClientRepository(PharmaGOContext db)
        {
            _db = db;
        }

        public void Add(Client client)
        {
            _db.Clients.Add(client);
            _db.SaveChanges();
        }

        public Client? GetClientByEmail(string email)
        {
            return _db.Clients.SingleOrDefault(u => u.Email == email);
        }
    }
}
