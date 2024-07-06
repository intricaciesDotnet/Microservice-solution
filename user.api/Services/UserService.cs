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
    public async Task<Result<User>> CreateAsync(UserDto input, CancellationToken cancellationToken)
    {
        try
        {
            if (input == null) throw new UserException(nameof(input));

            User User = new User
            {
                UserId = Guid.NewGuid(),
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                SecondaryContact = input.SecondaryContact,
                PrimaryContact = input.PrimaryContact,
                CreatedUtc = DateTime.UtcNow,
                UpdateUtc = DateTime.UtcNow,
            };

            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync();

            return Result<User>.OnSucess(User, HttpStatusCode.Created);
        }
        catch (UserException)
        {
            return Result<User>.OnFailure(Error.BadRequest);
        }
    }

    public async Task<Result<User>> DeleteByIdAsync(Guid guidId, CancellationToken cancellationToken)
    {
        try
        {
            User? ById = await _context.Users.Where(x => x.UserId == guidId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (ById != null)
            {
                _context.Users.Remove(ById);
            }
            await _context.SaveChangesAsync();

            return Result<User>.OnSucess(ById!, HttpStatusCode.OK);
        }
        catch (UserException)
        {
            return Result<User>.OnFailure(Error.BadRequest, HttpStatusCode.BadRequest);
        }
    }

    public async Task<Result<IEnumerable<User>>> GetAllUserAsync(CancellationToken cancellationToken)
        => Result<IEnumerable<User>>.OnSucess(await _context
            .Users
            .AsNoTracking()
            .ToListAsync(),
            HttpStatusCode.OK);
    

    public async Task<Result<User>> GetByIdAsync(Guid guidId, CancellationToken cancellationToken)
    {
        try
        {
            User? ById = await _context.Users.Where(x => x.UserId == guidId)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            if (ById != null)
            {
                return Result<User>.OnSucess(ById, HttpStatusCode.OK);
            }

            return Result<User>.OnFailure(Error.InvalidId, HttpStatusCode.NotFound);
            
        }
        catch (UserException)
        {
            return Result<User>.OnFailure(Error.BadRequest, HttpStatusCode.BadRequest);
        }
    }

    public async Task<Result<User>> UpdateAsync(Guid guidId, UserDto input, CancellationToken cancellationToken)
    {
        try
        {
            Result<User> ByGuidId = await GetByIdAsync(guidId, cancellationToken);

            if (ByGuidId != null)
            {
                User user = ByGuidId.Value;
                user.FirstName = input.FirstName;
                user.LastName = input.LastName;
                user.Email = input.Email;
                user.SecondaryContact = input.SecondaryContact;
                user.PrimaryContact = input.PrimaryContact;
                user.UpdateUtc = DateTime.UtcNow;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return Result<User>.OnSucess(user, HttpStatusCode.OK);
            }

            return Result<User>.OnFailure(Error.InvalidId, HttpStatusCode.NotFound);
        }
        catch (UserException)
        {
            return Result<User>.OnFailure(Error.BadRequest, HttpStatusCode.BadRequest);
        }
    }

}
