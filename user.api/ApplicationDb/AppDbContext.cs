using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using user.api.Abstractions.AssemblyReference;

namespace user.api.ApplicationDb;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserOrder> UserOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyRef.Reference);
    }
}
