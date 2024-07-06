using System.ComponentModel.DataAnnotations;

namespace Application.Core.Entities;

public class UserOrder : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}