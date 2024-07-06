using System.ComponentModel.DataAnnotations;

namespace Application.Core.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid FoodItemId { get; set; }
    public FoodItem FoodItem { get; set; }
    public int Quantity { get; set; }
}
