using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Repository.PostgreSQL;

public class PostgresDBContext : DbContext
{
    public PostgresDBContext(DbContextOptions<PostgresDBContext> options):base(options)
    {

    }

    public DbSet<OrderEntity> OrderEntities { get; set; }
    public DbSet<OrderItemsEntity> OrderItemsEntities { get; set; }

}