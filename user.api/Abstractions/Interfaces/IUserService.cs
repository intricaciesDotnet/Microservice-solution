using Application.Core.Entities;
using Application.Shared.Interfaces;
using Application.Shared.Messagings.Responses;
using user.api.DTOs;

namespace user.api.Abstractions.Interfaces;

public interface IUserService : IBaseServiceT<UserDto, Result<User>>
{
    Task<Result<IEnumerable<User>>> GetAllUserAsync(CancellationToken cancellationToken);
}

