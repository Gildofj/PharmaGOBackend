using Microsoft.EntityFrameworkCore;
using PharmaGO.Core.Entities.Base;
using PharmaGO.Core.Interfaces.Persistence.Base;

namespace PharmaGO.Infrastructure.Persistence.Base
{
    public class Repository<T>(PharmaGOContext db) : IRepository<T>
        where T : Entity
    {
        protected readonly PharmaGOContext Db = db;

        public async Task<T> AddAsync(T entity)
        {
            await Db.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            Db.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await Db.Set<T>().ToListAsync();

        public async Task<T?> FindByIdAsync(Guid id) => await Db.Set<T>().FindAsync(id);

        public Task UpdateAsync(T entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return  Task.CompletedTask;
        }
    }
}