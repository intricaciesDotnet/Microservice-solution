using Application.Core.Entities;
using Application.Shared.Messagings.Responses;
using Microsoft.EntityFrameworkCore;
using restaurant.api.ApplicationDb;
using restaurant.api.DTOs;
using user.api.Abstractions.Interfaces;

namespace restaurant.api.Services;

public class RestaurantService(AppDbContext context) : IRestaurantService
{
    private readonly AppDbContext _context = context;

    public async Task<Result<Restaurant>> CreateAsync(RestaurantDto input, CancellationToken cancellationToken)
    {
		try
		{
			if (input == null) throw new ArgumentNullException(nameof(input));

			Restaurant restaurant = new Restaurant
			{
				RestaurantId = Guid.NewGuid(),
				Name = input.Name,
				City = input.Address.City,
				State = input.Address.Street,
				ZipCode = input.Address.ZipCode,
				Street = input.Address.Street,
			};

			_context.Restaurants.Add(restaurant);

			await _context.SaveChangesAsync(cancellationToken);

			return Result<Restaurant>.OnSucess(restaurant, System.Net.HttpStatusCode.Created);
		}
		catch (Exception)
		{

			throw;
		}
    }

    public async Task<Result<IEnumerable<Restaurant>>> GetAllAsync(CancellationToken cancellation)
    {
		try
		{
			var list = await _context.Restaurants
				.AsNoTracking()
				.ToListAsync();

			if (list == null) throw new ArgumentNullException(nameof(list));

			return Result<IEnumerable<Restaurant>>.OnSucess(list, System.Net.HttpStatusCode.OK);

        }
		catch (Exception)
		{

			throw;
		}
    }
}


public class MenuItemService(AppDbContext context) : IMenuItemsService
{
    private readonly AppDbContext _context = context;
    public async Task<Result<IEnumerable<MenuItem>>> CreateAsync(MenuItemDtoList menuItems, CancellationToken cancellationToken)
    {
		try
		{
			Guid.TryParse(menuItems.RestaurantId, out Guid restaurantId);

			if (menuItems is {})
			{
				List<MenuItem> menus = new List<MenuItem>();

				foreach (MenuItemsDto item in menuItems.Items)
				{
					menus.Add(new MenuItem
					{
                        ItemId = Guid.NewGuid(),
                        RestaurantId = restaurantId,
                        Name = item.Name,
                        Description = item.Description,
                        Price = item.Price,
                        Category = item.Category,
						
                    });
				}

				if (menus.Count > 0) 
					_context.MenuItems.AddRange(menus);
				
				await _context.SaveChangesAsync();

				return Result<IEnumerable<MenuItem>>.OnSucess(_context.MenuItems, System.Net.HttpStatusCode.Created);
			}

            return Result<IEnumerable<MenuItem>>.OnFailure(Error.BadRequest, System.Net.HttpStatusCode.BadRequest);

        }
		catch (Exception)
		{

			throw;
		}
    }

    public async Task<Result<IEnumerable<MenuItem>>> GetAllAsync(CancellationToken cancellation)
    {
        try
        {
            var list = await _context.MenuItems
                .AsNoTracking()
                .ToListAsync();

            if (list == null) throw new ArgumentNullException(nameof(list));

            return Result<IEnumerable<MenuItem>>.OnSucess(list, System.Net.HttpStatusCode.OK);

        }
        catch (Exception)
        {

            throw;
        }
    }
}
