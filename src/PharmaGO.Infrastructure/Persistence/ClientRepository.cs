using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Core.Entities;
using Microsoft.EntityFrameworkCore;
using PharmaGO.Infrastructure.Persistence.Base;

namespace PharmaGO.Infrastructure.Persistence;

public class ClientRepository(PharmaGOContext db) : Repository<Client>(db), IClientRepository
{
    public async Task<Client?> GetClientByEmailAsync(string email)
    {
        return await Db.Client.SingleOrDefaultAsync(u => u.Email == email);
    }
}
