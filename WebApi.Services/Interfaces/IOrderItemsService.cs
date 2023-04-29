using WebApi.Entities;

namespace WebApi.Services.Interfaces
{
    public interface IOrderItemsService
    {
        ValueTask<OrderItemsEntity?> GetById(Guid id);
        Task<IEnumerable<OrderItemsEntity>> GetByIds(IEnumerable<Guid> ids);
        Task<IEnumerable<OrderItemsEntity>> GetByOrderId(Guid orderId);
        Task Insert(OrderItemsEntity entity);
        Task InsertCollection(IEnumerable<OrderItemsEntity> orderItems);
        Task Update(OrderItemsEntity entity);
        Task UpdateCollection(IEnumerable<OrderItemsEntity> orderItems);
        ValueTask<bool> Delete(Guid id);
    }
}
