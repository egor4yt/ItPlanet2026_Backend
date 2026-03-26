using System.Text;
using Launchpad.Api.Configuration.Options;
using Launchpad.Api.Filters;
using Launchpad.Api.HealthChecks;
using Launchpad.Api.Services;
using Launchpad.Api.Services.Interfaces;
using Launchpad.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Api.Configuration;

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
        ConfigureInfrastructure(builder.Services, builder.Configuration);
        ConfigureLogging(builder.Services, builder.Logging, builder.Configuration);
        ConfigureAuthorization(builder.Services, builder.Configuration);
    }

    private static void ConfigureAuthorization(IServiceCollection services, IConfiguration configuration)
    {
        var jwtOptions = new JwtOptions();
        configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
        services.Configure<JwtOptions>(x =>
        {
            x.Audience = jwtOptions.Audience;
            x.Issuer = jwtOptions.Issuer;
            x.Key = jwtOptions.Key;
            x.TokenLifetimeInHours = jwtOptions.TokenLifetimeInHours;
        });

        services.AddAuthorizationBuilder()
            .AddPolicy(JwtDetailsRole.Administrator, policy => policy.RequireRole(JwtDetailsRole.Administrator))
            .AddPolicy(JwtDetailsRole.Curator, policy => policy.RequireRole(JwtDetailsRole.Curator, JwtDetailsRole.Administrator))
            .AddPolicy(JwtDetailsRole.Employee, policy => { policy.RequireRole(JwtDetailsRole.Employee, JwtDetailsRole.Curator, JwtDetailsRole.Administrator); })
            .AddPolicy(JwtDetailsRole.Employer, policy => policy.RequireRole(JwtDetailsRole.Employer, JwtDetailsRole.Curator, JwtDetailsRole.Administrator));

        services
            .AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                    RoleClaimType = UserJwtClaimNames.ProfileRole
                };
            });
    }

    private static void ConfigureInfrastructure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
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