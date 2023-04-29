using WebApi.Entities;
using WebApi.Repository.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async ValueTask<OrderEntity?> GetById(Guid id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task Insert(OrderEntity entity)
        {
            await _orderRepository.Insert(entity);
        }

        public async Task Update(OrderEntity entity)
        {
            await _orderRepository.Update(entity);
        }
        public async ValueTask<bool> Delete(Guid id)
        {
            return await _orderRepository.Delete(id);
        }
    }
}
