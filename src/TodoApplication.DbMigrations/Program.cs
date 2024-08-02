
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System.Reflection;
using TodoApplication.DbMigrations.EFCore;

var logger = CreateLogger();
logger.Information("Migration Start");
var dbContextFactory = new ApplicationDbContextMigration();

try
{
    await using var appDbContext = dbContextFactory.CreateDbContext(args);

    var migration = appDbContext.GetService<IMigrator>();

    var pendingMigration = await appDbContext.Database.GetPendingMigrationsAsync();

    if (!pendingMigration.Any())
    {
        logger.Warning("No pending migrations found to generate SQL script");
    }
    else
    {
        var appliedMigrations = await appDbContext.Database.GetAppliedMigrationsAsync();
        var lastMigrations = appliedMigrations.LastOrDefault();
        var script = migration.GenerateScript(lastMigrations);
        logger.Information("Generated Script \n {script}", script);
        script = script.Replace("{", "{{").Replace("}", "}}");
        await appDbContext.Database.ExecuteSqlRawAsync(script);
    }
}
catch (Exception e)
{
    Log.Error(e, "Something went wrong with error {error}", e.Message);
}
finally
{
    logger.Information("Migration End");
}

static Logger CreateLogger()
{
    var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;

    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json",
            optional: false, reloadOnChange: true);

    return new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Build())
        .MinimumLevel.Debug()
        .CreateLogger();
}
