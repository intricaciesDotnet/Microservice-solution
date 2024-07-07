using Application.Core.Entities;
using Application.Shared.Interfaces;
using Application.Shared.Messagings.Responses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using user.api.Abstractions.Interfaces;
using user.api.ApplicationDb;
using user.api.DTOs;
using user.api.Exceptions;

namespace user.api.Services;

public class UserService(AppDbContext context) : IUserService
{
    private readonly AppDbContext _context = context;

    public async Task<Result<List<PaymentMethod>>> AddPaymentMethod(List<AddPaymentMethodsByUserId> paymentMethodDto, CancellationToken cancellationToken)
    {
        try
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            if (paymentMethodDto is {})
            {
                foreach(var paymentmethod in  paymentMethodDto)
                {
                    if (Guid.TryParse(paymentmethod.UserId, out var userId))
                    {
                        paymentMethods.Add(new()
                        {
                            Id = Guid.NewGuid(),
                            Type = paymentmethod.Type,
                            Provider = paymentmethod.Provider,
                            ExpiryDate = paymentmethod.ExpiryDate,
                            CardNumber = paymentmethod.CardNumber,
                            Cvv = paymentmethod.Cvv,
                            UserId = userId
                        });
                    }
                }

                _context.PaymentMethods.AddRange(paymentMethods);
                await _context.SaveChangesAsync();

                return Result<List<PaymentMethod>>.OnSucess(paymentMethods, HttpStatusCode.Created);
            }

            return Result<List<PaymentMethod>>.OnFailure(Error.BadRequest, HttpStatusCode.BadRequest);

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Result<User>> CreateAsync(UserDto input, CancellationToken cancellationToken)
    {
        try
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            
            User user = new User
            {
                UserId = Guid.NewGuid(),
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                Street = input.Address.Street,
                City = input.Address.City,
                ZipCode = input.Address.ZipCode,
                State = input.Address.State,
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Result<User>.OnSucess(user, HttpStatusCode.Created);
        }
        catch (UserException)
        {
            throw new NotImplementedException();
        }
    }

    public async Task<Result<IEnumerable<User>>> GetAllUserAsync(CancellationToken cancellationToken)
    {
        try
        {
            var list =  await _context
                .Users
                .AsNoTracking()
                .ToListAsync();

            return Result<IEnumerable<User>>.OnSucess(list, HttpStatusCode.OK);
        }
        catch (UserException)
        {
            throw new NotImplementedException();
        }
    }
}
