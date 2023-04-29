using WebApi.Entities;
using WebApi.Repository.Interfaces;

namespace WebApi.Repository.PostgreSQL
{
    public class OrderRepository : BaseRepository<OrderEntity>, IOrderRepository
    {
        public OrderRepository(PostgresDBContext context) : base(context)
        {
        }

        public override Task Insert(OrderEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            entity.Status = (int)Status.New;
            return base.Insert(entity);
        }
    }
}
