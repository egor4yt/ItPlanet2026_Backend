using Launchpad.Candidates.Api.Configuration.Options;
using Launchpad.Candidates.Api.HealthChecks;
using Launchpad.Candidates.Api.Services;
using Launchpad.Candidates.Api.Services.Interfaces;
using Launchpad.Candidates.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Candidates.Api.Configuration;

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
    }

    /// <summary>
    ///     API configuration
    /// </summary>
    /// <param name="builder">App builder instance</param>
    public static void ConfigureApi(this IHostApplicationBuilder builder)
    {
        ConfigureApiServices(builder.Services, builder.Configuration);
        ConfigureLogging(builder.Services, builder.Logging, builder.Configuration);
        ConfigureAuthorization(builder.Services, builder.Configuration);
    }

    private static void ConfigureAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        var keycloakOptions = configuration
            .GetRequiredSection(ConfigurationKeys.Keyckloak)
            .Get<KeycloakOptions>()!;

        services.AddAuthorizationBuilder();

        services
            .AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = $"{keycloakOptions.AuthorityBaseUrl}/realms/{keycloakOptions.Realm}";
                options.Audience = keycloakOptions.Client;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = $"{keycloakOptions.IssuerBaseUrl}/realms/{keycloakOptions.Realm}",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };

                options.RequireHttpsMetadata = false;
            });
    }

    private static void ConfigureApiServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHealthChecks().AddCheck<ApiHealthCheck>("API");
    }

    private static void ConfigureLogging(IServiceCollection services, ILoggingBuilder logging, IConfiguration configuration)
    {
        logging.ClearProviders();
        logging.AddSerilog();
        services.AddSerilog();

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}