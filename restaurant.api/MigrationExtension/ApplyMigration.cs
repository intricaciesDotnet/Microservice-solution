using Microsoft.EntityFrameworkCore;
using restaurant.api.ApplicationDb;

namespace restaurant.api.MigrationExtension;

public static class ApplyMigration
{
    public static void ApplyMigrate(this IApplicationBuilder applicationBuilder)
    {
        using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();

        using AppDbContext appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>()!;

        appDbContext.Database.Migrate();
    }
}
