using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Constants;

namespace TodoApplication.DbMigrations.EFCore;

internal class ApplicationDbContextMigration : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json",
                optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        var timeOutInSecond = Convert.ToInt32(config["DbSetting:Timeout"]!);
        var migrationTableName = config["DbSetting:MigrationTableName"]!;
        var connectionString = config["DbSetting:ConnectionString"]!;

        var dbOptionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(connectionString, option =>
            {
                option.CommandTimeout(timeOutInSecond)
                    .MigrationsAssembly(AssemblyReference.Assembly.GetName().Name)
                    .MigrationsHistoryTable(migrationTableName, Schemas.Migration);

            });

        return new ApplicationDbContext(dbOptionBuilder.Options);
    }
}
