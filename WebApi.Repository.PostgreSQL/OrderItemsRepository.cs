using WebApi.Entities;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository.PostgreSQL
{
    public class OrderItemsRepository : BaseRepository<OrderItemsEntity>,IOrderItemsRepository
    {
        private readonly PostgresDBContext _context;
        public OrderItemsRepository(PostgresDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItemsEntity>> GetByOrderId(Guid orderId)
        {
            var orderItems = _context.OrderItemsEntities.Where(x => x.OrderId == orderId).ToList();
            return orderItems;
        }
    }
}
