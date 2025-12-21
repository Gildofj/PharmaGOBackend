using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence.Base;

namespace PharmaGO.Core.Interfaces.Persistence;
public interface IClientRepository : IRepository<Client>
{
    Task<Client?> FindByEmailAsync(string email);
    Task<bool> ExistsByCpfAsync(string cpf);
}
