using Application.Core.Entities;
using Microsoft.EntityFrameworkCore;
using restaurant.api.Abstractions.AssemblyReference;

namespace restaurant.api.ApplicationDb;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyRef.Reference);
    }
}
