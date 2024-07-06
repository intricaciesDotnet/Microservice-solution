using Application.Core.Entities;
using Application.Shared.Messagings.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using user.api.Abstractions;
using user.api.Abstractions.Interfaces;
using user.api.DTOs;

namespace user.api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Helpers.AllUser, async (IUserService userService, CancellationToken cancellation) =>
        {
            Result<IEnumerable<User>> users = await userService.GetAllUserAsync(cancellation);
            return users.IsSuccess ?
                 Results.Ok(users) :
                 Results.NotFound(users);
        });

        routeBuilder.MapPost(Helpers.CreateUser, async ([FromBody]
            UserDto userDto,
            IUserService userService,
            CancellationToken cancellation) =>
        {
            if (userDto != null)
            {
                Result<User> createdUser = await userService.CreateAsync(userDto, cancellation);

                return createdUser.IsSuccess ?
                Results.Ok(createdUser) :
                Results.BadRequest(createdUser);
            }

            return Results.BadRequest(Result<UserDto>.OnFailure(Error.BadRequest, HttpStatusCode.BadRequest));
        });

        routeBuilder.MapPut(Helpers.UpdateUser, async ([FromQuery] string guidId,
            [FromBody] UserDto userDto,
            IUserService userService,
            CancellationToken cancellation) =>
        {
            if (!string.IsNullOrEmpty(guidId))
            {
                if (Guid.TryParse(guidId, out var guid))
                {
                    Result<User> user = await userService.UpdateAsync(guid, userDto, cancellation);
                    return user.IsSuccess ?
                    Results.Ok(user) :
                    Results.BadRequest(user);
                }
            }
            return Results.BadRequest(Result<UserDto>.OnFailure(Error.InvalidId, HttpStatusCode.NotFound));
        });


        routeBuilder.MapGet(Helpers.GetUserByUniqueId, async ([FromQuery] string guidId,
            IUserService userService,
            CancellationToken cancellation) =>
        {
            if (!string.IsNullOrEmpty(guidId))
            {
                if (Guid.TryParse(guidId, out var guid))
                {
                    Result<User> user = await userService.GetByIdAsync(guid, cancellation);
                    return user.IsSuccess ?
                    Results.Ok(user) :
                    Results.BadRequest(user);
                }
            }

            return Results.BadRequest(Result<UserDto>.OnFailure(Error.InvalidId, HttpStatusCode.NotFound));
        });


        routeBuilder.MapDelete(Helpers.DeleteUser, async ([FromQuery] string guidId,
            IUserService userService,
            CancellationToken cancellation) =>
        {
            if (!string.IsNullOrEmpty(guidId))
            {
                if (Guid.TryParse(guidId, out var guid))
                {
                    Result<User> user = await userService.DeleteByIdAsync(guid, cancellation);
                    return user.IsSuccess ?
                    Results.Ok(user) :
                    Results.BadRequest(user);
                }
            }
            return Results.BadRequest(Result<UserDto>.OnFailure(Error.InvalidId, HttpStatusCode.NotFound));
        });
    }
}
