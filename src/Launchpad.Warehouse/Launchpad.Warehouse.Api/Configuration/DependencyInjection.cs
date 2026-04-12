using Launchpad.Warehouse.Api.HealthChecks;
using Launchpad.Warehouse.Api.Middlewares;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Warehouse.Api.Configuration;

/// <summary>
///     API configuration
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    ///     Add HTTP requests logging
    /// </summary>
    /// <param name="app">App instance</param>
    public static void UseRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging(options =>
        {
            options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} Status={StatusCode} Elapsed time={Elapsed} ms";
            options.GetLevel = (_, _, _) => LogEventLevel.Debug;
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            };
        });
        app.UseMiddleware<LoggingMiddleware>();
    }

    /// <summary>
    ///     API configuration
    /// </summary>
    /// <param name="builder">App builder instance</param>
    public static void ConfigureApi(this IHostApplicationBuilder builder)
    {
        ConfigureApiServices(builder.Services, builder.Configuration);
        ConfigureLogging(builder.Services, builder.Logging, builder.Configuration);
        ConfigureObservability(builder.Services);
    }

    private static void ConfigureApiServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();
        services.AddHealthChecks().AddCheck<ApiHealthCheck>("API");
    }

    private static void ConfigureLogging(IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        logging.ClearProviders();
        logging.AddSerilog();
        services.AddSerilog();

        Log.Logger = new LoggerConfiguration()
            .Enrich.WithProperty("ServiceName", "warehouse.api")
            .Enrich.WithEnvironment(Shared.ConfigurationKeys.Environment)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static void ConfigureObservability(IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddPrometheusExporter()
                    .AddMeter("Launchpad.Warehouse");
            });
    }
}