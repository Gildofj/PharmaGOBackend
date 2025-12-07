using Microsoft.EntityFrameworkCore;
using PharmaGOBackend.Core.Entities.Base;
using PharmaGOBackend.Core.Interfaces.Persistence.Base;

namespace PharmaGOBackend.Infrastructure.Persistence.Base
{
    public class Repository<T>(PharmaGOContext db) : IRepository<T>
        where T : Entity
    {
        protected readonly PharmaGOContext Db = db;

        public async Task<T> AddAsync(T entity)
        {
            await Db.Set<T>().AddAsync(entity);
            await Db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            Db.Set<T>().Remove(entity);
            await Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await Db.Set<T>().ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) => await Db.Set<T>().FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            await Db.SaveChangesAsync();
        }
    }
}