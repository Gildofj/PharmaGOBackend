using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Persistence.Base;

namespace PharmaGOBackend.Core.Persistence;
public interface IClientRepository : IRepository<Client>
{
    Task<Client?> GetClientByEmailAsync(string email);
}
