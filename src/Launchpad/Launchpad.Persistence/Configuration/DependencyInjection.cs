using Launchpad.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
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

        var connectionString = app.Configuration.GetRequiredSection(ConfigurationKeys.SqlDatabaseConnectionString);
        if (connectionString == null) throw new InvalidOperationException("The connection string is missing in the configuration file.");

        if (app.Environment.IsDevelopment())
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
            );
        else
            app.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString.Value)
                    .LogTo(Log.Information, LogLevel.Information, DbContextLoggerOptions.Id | DbContextLoggerOptions.Category)
            );

        return app;
    }
}