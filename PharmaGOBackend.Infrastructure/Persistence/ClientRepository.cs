using PharmaGOBackend.Core.Persistence;
using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Infrastructure.Persistence.Base;
using Microsoft.EntityFrameworkCore;

namespace PharmaGOBackend.Infrastructure.Persistence;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(PharmaGOContext db) : base(db)
    {
    }

    public async Task<Client?> GetClientByEmail(string email)
    {
        return await _db.Client.SingleOrDefaultAsync(u => u.Email == email);
    }
}
