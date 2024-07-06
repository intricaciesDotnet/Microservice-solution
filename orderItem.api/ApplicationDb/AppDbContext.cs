using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace orderItem.api.ApplicationDb;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<OrderItem> OrderItems { get; set; }
}
