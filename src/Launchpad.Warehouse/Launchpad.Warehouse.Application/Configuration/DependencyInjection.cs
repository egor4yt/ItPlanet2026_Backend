using System.Reflection;
using FluentValidation;
using Launchpad.Warehouse.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Launchpad.Warehouse.Application.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigureApplication(this IHostApplicationBuilder app)
    {
        var assembly = Assembly.GetExecutingAssembly();

        app.Services.AddValidatorsFromAssembly(assembly);
        app.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);

            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return app;
    }
}