using WebApi.Entities;

namespace WebApi.Repository.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        ValueTask<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetByIds(IEnumerable<Guid> ids);
        Task Insert(T entity);
        Task InsertCollection(IEnumerable<T> entities);
        Task Update(T entity);
        Task UpdateCollection(IEnumerable<T> entities);
        ValueTask<bool> Delete(Guid id);
    }
}
