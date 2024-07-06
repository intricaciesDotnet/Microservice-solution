using System.ComponentModel.DataAnnotations;

namespace Application.Core.Entities;

public class FoodItem : BaseEntity
{
    [Key]
    public Guid FoodItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
