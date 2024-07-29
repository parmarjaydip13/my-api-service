using Domain;
using Microsoft.EntityFrameworkCore;

namespace TodoApplication.API.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigration(this IApplicationBuilder app)
    {
        try
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

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
