
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System.Reflection;
using TodoApplication.DbMigrations.EFCore;

Log.Logger = CreateLogger();
Log.Information("Migration Start");
var dbContextFactory = new ApplicationDbContextMigration();

try
{
    await using (var appDbContext = dbContextFactory.CreateDbContext(args))
    {
        var migrator = appDbContext.GetService<IMigrator>();

        var pendingMigration = await appDbContext.Database.GetPendingMigrationsAsync();

        if (!pendingMigration.Any())
        {
            Log.Warning("No pending migrations found to generate SQL script");
        }

        var appliedMigrations = await appDbContext.Database.GetAppliedMigrationsAsync();
        var lastMigrations = appliedMigrations.LastOrDefault();
        var script = migrator.GenerateScript(lastMigrations);
        Log.Information("Generated Script {script}", script);
        script = script.Replace("{", "{{").Replace("}", "}}");
        await appDbContext.Database.ExecuteSqlRawAsync(script);
    }

    Log.Information("Migration End");
}
catch (Exception e)
{
    Log.Error(e, "Something went wrong with error {error}", e.Message);
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
        .WriteTo.Console()
        .CreateLogger();
}
