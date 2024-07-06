using Application.Core.Entities;

namespace user.api.DTOs;

public sealed class UserDto : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PrimaryContact { get; set; } = string.Empty;
    public string SecondaryContact { get; set; } = string.Empty;
}
