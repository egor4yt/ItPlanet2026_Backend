using System.Reflection;
using FluentValidation;
using Launchpad.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Launchpad.Application.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigureApplication(this IHostApplicationBuilder app)
    {
        var assembly = Assembly.GetExecutingAssembly();

        app.Services.AddValidatorsFromAssembly(assembly);
        app.Services.AddMediatR(config => { config.RegisterServicesFromAssembly(assembly); });
        app.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        app.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return app;
    }
}