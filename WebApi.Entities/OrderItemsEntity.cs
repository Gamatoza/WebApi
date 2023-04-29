using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class OrderItemsEntity : BaseEntity
{
    public int Quantity { get; set; }
    public Guid OrderId { get; set; }
    [ForeignKey("OrderId")]
    public OrderEntity OrderEntity { get; set; }
}