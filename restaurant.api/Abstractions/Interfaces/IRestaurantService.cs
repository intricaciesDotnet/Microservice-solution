using Application.Core.Entities;
using Application.Shared.Interfaces;
using Application.Shared.Messagings.Responses;
using restaurant.api.DTOs;

namespace user.api.Abstractions.Interfaces;

public interface IRestaurantService : IBaseServiceT<RestaurantDto, Result<Restaurant>>
{
    Task<Result<IEnumerable<Restaurant>>> GetAllAsync(CancellationToken cancellation);

}

public interface IMenuItemsService : IBaseServiceT<MenuItemDtoList, Result<IEnumerable<MenuItem>>>
{
    Task<Result<IEnumerable<MenuItem>>> GetAllAsync(CancellationToken cancellation);
}

