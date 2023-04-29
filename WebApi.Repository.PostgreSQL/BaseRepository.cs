using WebApi.Repository.Interfaces;
using WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repository.PostgreSQL
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly PostgresDBContext _context;

        public BaseRepository(PostgresDBContext context)
        {
            _context = context;
        }

        public ValueTask<T?> GetById(Guid id)
        {
            return _context.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids)
        {
            return await _context.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public virtual async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task InsertCollection(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public virtual Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        public virtual Task UpdateCollection(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            return _context.SaveChangesAsync();
        }

        public virtual async ValueTask<bool> Delete(Guid id)
        {
            var obj = await _context.Set<T>().FindAsync(id);
            if (obj != null)
            {
                obj.IsDeleted = true;
                await Update(obj);
                return true;
            }
            return false;
        }
    }
}
