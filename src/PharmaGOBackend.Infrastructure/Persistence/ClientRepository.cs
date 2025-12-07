using PharmaGOBackend.Core.Interfaces.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class ClientRepository(PharmaGOContext db) : Repository<Client>(db), IClientRepository
{
    public async Task<Client?> GetClientByEmailAsync(string email)
    {
        return await Db.Client.SingleOrDefaultAsync(u => u.Email == email);
    }
}
