using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Entities;
using Microsoft.EntityFrameworkCore;
using PharmaGO.Infrastructure.Persistence.Base;

namespace PharmaGO.Infrastructure.Persistence;

public class ClientRepository(PharmaGOContext db) : Repository<Client>(db), IClientRepository
{
    public async Task<Client?> FindByEmailAsync(string email)
    {
        return await Db.Clients.SingleOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> ExistsByCpfAsync(string email)
    {
        return await Db.Clients.AnyAsync(c => c.Email == email);
    }
}
