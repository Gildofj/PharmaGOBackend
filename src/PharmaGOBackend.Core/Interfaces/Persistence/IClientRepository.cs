using PharmaGOBackend.Core.Entities;
using PharmaGOBackend.Core.Interfaces.Persistence.Base;

namespace PharmaGOBackend.Core.Interfaces.Persistence;
public interface IClientRepository : IRepository<Client>
{
    Task<Client?> GetClientByEmailAsync(string email);
}
