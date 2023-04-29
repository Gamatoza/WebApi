namespace WebApi.Entities;

public class OrderEntity : BaseEntity
{
    public int Status { get; set; }
    public DateTime Created { get; set; }
}