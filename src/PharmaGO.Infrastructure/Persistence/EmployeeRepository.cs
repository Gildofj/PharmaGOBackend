using Microsoft.EntityFrameworkCore;
using PharmaGO.Core.Entities;
using PharmaGO.Core.Interfaces.Persistence;
using PharmaGO.Infrastructure.Persistence.Base;

namespace PharmaGO.Infrastructure.Persistence;

public class EmployeeRepository(PharmaGOContext db) : Repository<Employee>(db), IEmployeeRepository
{
    public async Task<Employee?> FindEmployeeByEmailAsync(string email)
    {
        return await Db.Employees.SingleOrDefaultAsync(u => u.Email == email);
    }
}