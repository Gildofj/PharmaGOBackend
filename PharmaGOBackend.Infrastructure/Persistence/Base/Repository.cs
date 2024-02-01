
using Microsoft.EntityFrameworkCore;
using PharmaGOBackend.Core.Entities.Base;
using PharmaGOBackend.Core.Interfaces.Persistence.Base;

namespace PharmaGOBackend.Infrastructure.Persistence.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly PharmaGOContext _db;

        public Repository(PharmaGOContext db)
        {
            _db = db;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _db.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(Guid id) => await _db.Set<T>().FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
