using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class OrderItemsModel
    {
        public Guid Id { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
    }
}
