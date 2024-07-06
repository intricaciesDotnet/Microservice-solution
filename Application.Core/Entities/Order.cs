using System.ComponentModel.DataAnnotations;

namespace Application.Core.Entities;

public class Order : BaseEntity
{
    [Key]
    public Guid OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<UserOrder> UserOrders { get; set; }
}

