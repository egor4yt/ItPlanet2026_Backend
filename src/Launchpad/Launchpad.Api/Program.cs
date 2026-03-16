using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/health-check", () => "ok")
    .WithName("GetWeatherForecast");

app.Run();