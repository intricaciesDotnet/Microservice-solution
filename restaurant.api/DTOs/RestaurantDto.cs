 namespace restaurant.api.DTOs;

public record RestaurantDto(string Name, Address Address);

public record Address(string Street, string City, string State, string ZipCode);


public record MenuItemsDto(
    string Name,
    string Description,
    decimal Price,
    string Category
   );


public record MenuItemDtoList(List<MenuItemsDto> Items, string RestaurantId);