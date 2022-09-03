using PharmaGOBackend.Application.Common.Interfaces.Persistence;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class ClientRepository : IClientRepository
{
    private readonly PharmaGOContext _db;

    public ClientRepository(PharmaGOContext db)
    {
        _db = db;
    }

    public Client Add(Client client)
    {
        _db.Clients.Add(client);
        _db.SaveChanges();
        return client;
    }

    public Client? GetClientByEmail(string email)
    {
        return _db.Clients.SingleOrDefault(u => u.Email == email);
    }
}
