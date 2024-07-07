using Application.Core.Entities;
using Application.Shared.Messagings.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using user.api.Abstractions;
using user.api.Abstractions.Interfaces;
using user.api.DTOs;
using user.api.Exceptions;
using System.Linq;

namespace user.api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoint(this IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Helpers.AllUser, async (IUserService userService, 
            CancellationToken cancellation) =>
        {
            try
            {
                Result<IEnumerable<User>> users = await userService.GetAllUserAsync(cancellation);

                return users.IsSuccess ?
                     Results.Ok(users) :
                     Results.NotFound(users);
            }
            catch (UserException)
            {
                return Results.Ok(Result<List<PaymentMethod>>.OnFailure(Error.Failed));
            }
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



        routeBuilder.MapPost(Helpers.AddPaymentMethods, async (
            [FromBody] AddPaymentMethodsByUserIdList paymentMethods,
            IUserService userService,
            CancellationToken cancellation) =>
        {
            try
            {
                if (paymentMethods != null && paymentMethods.AddPaymentMethodsByUserId.Any())
                {
                    Result<List<PaymentMethod>> payments = await userService.AddPaymentMethod(paymentMethods.AddPaymentMethodsByUserId, cancellation);

                    return Results.Ok(payments);
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
