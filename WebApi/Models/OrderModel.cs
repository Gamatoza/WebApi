using System.Text.Json.Serialization;
using WebApi.Entities;

namespace WebApi.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
        public string Created { get; set; }
        public IEnumerable<OrderItemsModel> Lines { get; set; }
    }
}
