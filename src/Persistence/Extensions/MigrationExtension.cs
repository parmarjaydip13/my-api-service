using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigration(IServiceCollection serviceCollection)
    {
        try
        {
            using ApplicationDbContext dbContext =
                serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
