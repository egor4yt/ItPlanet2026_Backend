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
        app.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>("Database");

        var connectionString = app.Configuration.GetSection(ConfigurationKeys.SqlDatabaseConnectionString);
        if (string.IsNullOrWhiteSpace(connectionString.Value)) throw new InvalidOperationException("The connection string is missing in the configuration file.");

        var environment = app.Configuration.GetSection(ConfigurationKeys.Environment);
        if (string.IsNullOrWhiteSpace(environment.Value)) environment.Value = Shared.Environments.Production;

        if (environment.Value == Shared.Environments.Development)
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
        else if (environment.Value == Shared.Environments.IntegrationTests)
        {
            Log.Warning("Warning PendingModelChangesWarning will be ignored");

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
        else if (environment.Value == Shared.Environments.Production)
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
            );
        else
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );

        return app;
    }
}