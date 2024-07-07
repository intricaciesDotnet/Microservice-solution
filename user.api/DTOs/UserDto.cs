using Application.Core.Entities;

namespace user.api.DTOs;

public sealed class UserDto : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Address Address { get; set; }

}

public sealed class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
}

public class PaymentMethodDto : BaseEntity
{
    public string Type { get; set; }
    public string Provider { get; set; }
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string Cvv { get; set; }
    public string UserId { get; set; }
}

public sealed class AddPaymentMethodsByUserId : PaymentMethodDto
{
    public string UserId { get; set; }
}

public sealed class AddPaymentMethodsByUserIdList
{ 
    public List<AddPaymentMethodsByUserId> AddPaymentMethodsByUserId { get; set; }
}


