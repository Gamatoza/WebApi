using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IOrderService
    {
        ValueTask<OrderEntity?> GetById(Guid id);
        Task Insert(OrderEntity entity);
        Task Update(OrderEntity entity);
        ValueTask<bool> Delete(Guid id);
    }
}
