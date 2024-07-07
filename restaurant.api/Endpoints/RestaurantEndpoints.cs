using Application.Core.Entities;
using Application.Shared.Messagings.Responses;
using Microsoft.AspNetCore.Mvc;
using restaurant.api.DTOs;
using user.api.Abstractions.Interfaces;

namespace restaurant.api.Endpoints;

public static class RestaurantEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("api/restaurant/new", async (
            [FromBody] RestaurantDto restaurant,
            IRestaurantService restaurantService,
            CancellationToken cancellation) =>
        {
            try
            {
                if (restaurant is { })
                {
                    Result<Restaurant> result = await restaurantService.CreateAsync(restaurant, cancellation);

                    return result.IsSuccess ?
                    Results.Ok(result) :
                    Results.BadRequest(result);
                }

                return Results.BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        });

        routeBuilder.MapPost("api/restaurant/new-menu-items", async (
            [FromBody] MenuItemDtoList menuItems,
            IMenuItemsService menuItemsService,
            CancellationToken cancellation) =>
        {
            try
            {
                if (menuItems is {})
                {
                    Guid.TryParse(menuItems.RestaurantId, out Guid validGuidId);

                    Result<IEnumerable<MenuItem>> resultMenuItems = await menuItemsService.CreateAsync(menuItems, cancellation);

                    return resultMenuItems.IsSuccess ?
                    Results.Ok(resultMenuItems):
                    Results.BadRequest(resultMenuItems);
                }
                return Results.BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        });


    }
}
