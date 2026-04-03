using Launchpad.Candidates.Api.Configuration;
using Launchpad.Candidates.Api.Configuration.Options;
using Launchpad.Candidates.Api.Services;
using Launchpad.Candidates.Infrastructure.Configuration;
using Launchpad.Candidates.Infrastructure.Persistence.Configuration;
using Launchpad.Candidates.Shared;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
        .AddEnvironmentVariables();

    builder.ConfigureApi();
    builder.ConfigureInfrastructure();
    // builder.ConfigureApplication();

    var app = builder.Build();

    var keycloakOptions = app.Configuration
        .GetRequiredSection(ConfigurationKeys.Keyckloak)
        .Get<KeycloakOptions>();

    app.UseRequestLogging();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        c.OAuthClientId(keycloakOptions!.Client);
        c.OAuthUsePkce();
    });

    var appUrls = app.Configuration["applicationUrl"]?.Split(';')
                  ?? app.Configuration.GetValue<string>("urls")?.Split(';')
                  ?? [];

    var environment = app.Configuration.GetSection(ConfigurationKeys.Environment);
    if (string.IsNullOrWhiteSpace(environment.Value)) environment.Value = Launchpad.Candidates.Shared.Environments.Production;

#pragma warning disable CA1873
    app.Logger.LogInformation("Application started with environment {Environment}", environment.Value);
    app.Logger.LogInformation("Application listening on {Addresses}", appUrls.Select(object? (x) => $"{x}/health"));
    app.Logger.LogInformation("Swagger documentation listening on {SwaggerAddresses}", appUrls.Select(object? (x) => $"{x}/swagger/index.html"));
#pragma warning restore CA1873

    var corsOrigin = builder.Configuration.GetSection("CorsOrigins");
    if (!string.IsNullOrWhiteSpace(corsOrigin.Value))
        app.UseCors(x =>
            x.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(corsOrigin.Value)
                .AllowCredentials());

    app.UseInitializeDatabase();

    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = HealthCheckService.WriterHealthCheckResponse,
        AllowCachingResponses = false
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException && ex.Source != "Microsoft.EntityFrameworkCore.Design") // see https://github.com/dotnet/efcore/issues/29923
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}