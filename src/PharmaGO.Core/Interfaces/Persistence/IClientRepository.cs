using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence.Base;

namespace PharmaGO.Core.Interfaces.Persistence;
public interface IClientRepository : IRepository<Client>
{
    Task<Client?> FindClientByEmailAsync(string email);
}
