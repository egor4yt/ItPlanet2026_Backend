using Launchpad.Api.Configuration;
using Launchpad.Api.Services;
using Launchpad.Persistence.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureApi();
    builder.ConfigurePersistence();
    // builder.ConfigureApplication();

    var app = builder.Build();

    app.UseRequestLogging();
    app.UseSwagger();
    app.UseSwaggerUI();

    var appUrls = builder.Configuration["applicationUrl"]?.Split(';')
                  ?? builder.Configuration.GetValue<string>("urls")?.Split(';')
                  ?? [];

#pragma warning disable CA1873
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