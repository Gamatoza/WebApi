using WebApi.Entities;
using WebApi.Repository.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class OrderItemsService : IOrderItemsService
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        public OrderItemsService(IOrderItemsRepository orderRepository)
        {
            _orderItemsRepository = orderRepository;
        }

        public async ValueTask<OrderItemsEntity?> GetById(Guid id)
        {
            return await _orderItemsRepository.GetById(id);
        }
        public async Task<IEnumerable<OrderItemsEntity>> GetByIds(IEnumerable<Guid> ids)
        {
            return await _orderItemsRepository.GetByIds(ids);
        }

        public async Task<IEnumerable<OrderItemsEntity>> GetByOrderId(Guid orderId)
        {
            return await _orderItemsRepository.GetByOrderId(orderId);
        }

        public async Task Insert(OrderItemsEntity entity)
        {
            await _orderItemsRepository.Insert(entity);
        }

        public async Task InsertCollection(IEnumerable<OrderItemsEntity> orderItems)
        {
            await _orderItemsRepository.InsertCollection(orderItems);
        }

        public async Task Update(OrderItemsEntity entity)
        {
            await _orderItemsRepository.Update(entity);
        }

        public async Task UpdateCollection(IEnumerable<OrderItemsEntity> entities)
        {
            await _orderItemsRepository.UpdateCollection(entities);
        }

        public async ValueTask<bool> Delete(Guid id)
        {
            return await _orderItemsRepository.Delete(id);
        }
    }
}
