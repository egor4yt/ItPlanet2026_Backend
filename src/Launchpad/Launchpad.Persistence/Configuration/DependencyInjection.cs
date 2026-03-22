using Launchpad.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Launchpad.Persistence.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigurePersistence(this IHostApplicationBuilder app)
    {
        Log.Warning("1");

        app.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>("Database");
        Log.Warning("2");

        var connectionString = app.Configuration.GetSection(ConfigurationKeys.SqlDatabaseConnectionString);
        Log.Warning("3");
        if (string.IsNullOrWhiteSpace(connectionString.Value)) throw new InvalidOperationException("The connection string is missing in the configuration file.");
        Log.Warning("4");

        var environment = app.Configuration.GetSection(ConfigurationKeys.Environment);
        Log.Warning("5");
        Log.Warning("env: {Env}", environment.Value);
        if (string.IsNullOrWhiteSpace(environment.Value)) environment.Value = Shared.Environments.Production;
        Log.Warning("6");
        Log.Warning("env: {Env}", environment.Value);

        if (environment.Value == Shared.Environments.Development)
        {
            Log.Warning("7");
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
        }
        else if (environment.Value == Shared.Environments.IntegrationTests)
        {
            Console.WriteLine("Enabled ignoring PendingModelChangesWarning");
            Log.Warning("Enabled ignoring PendingModelChangesWarning");

            // https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-9.0/breaking-changes#mitigations
            // There are several common situations when this exception can be thrown:
            // The migrations are generated, modified or chosen dynamically by replacing some of the EF services.
            // Mitigation: The warning is a false positive in this case and should be suppressed

            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
                    .ConfigureWarnings(warnings =>
                        warnings.Ignore(RelationalEventId.PendingModelChangesWarning)
                    )
            );
        }
        else
        {
            Log.Warning("8");
            
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
            );
        }

        return app;
    }
}