using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace Application.Core.Entities;

public class BaseEntity
{
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdateUtc { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

}

public class User : Address
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
   
    public ICollection<PaymentMethod> PaymentMethods { get; set; }
}

public class PaymentMethod
{
    public Guid Id { get; set; }  // Primary Key
    public string Type { get; set; }
    public string Provider { get; set; }
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string Cvv { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}


public class Restaurant : Address
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; }
    public ICollection<MenuItem> Menu { get; set; }
}

public class MenuItem
{
    public Guid ItemId { get; set; }  // Primary Key
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}