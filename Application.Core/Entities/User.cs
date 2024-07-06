using System.ComponentModel.DataAnnotations;

namespace Application.Core.Entities;

public class User : BaseEntity
{
    [Key]
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PrimaryContact { get; set; } = string.Empty;
    public string SecondaryContact { get; set; }  = string.Empty;

    public ICollection<UserOrder> UserOrders { get; set; }

}
