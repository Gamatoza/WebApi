using WebApi.Entities;

namespace WebApi.Repository.Interfaces
{
    public interface IOrderItemsRepository : IRepository<OrderItemsEntity>
    {
        Task<IEnumerable<OrderItemsEntity>> GetByOrderId(Guid orderId);
    }
}
