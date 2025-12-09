using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence.Base;

namespace PharmaGO.Core.Interfaces.Persistence;

public interface IEmployeeRepository : IRepository<Employee>
{
    Task<Employee?> GetEmployeeByEmailAsync(string email);
}