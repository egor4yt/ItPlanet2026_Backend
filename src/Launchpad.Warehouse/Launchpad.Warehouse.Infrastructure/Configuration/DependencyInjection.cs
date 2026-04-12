using Launchpad.Warehouse.Infrastructure.MessageBroker.Kafka.Configuration;
using Launchpad.Warehouse.Infrastructure.Persistence.Configuration;
using Microsoft.Extensions.Hosting;

namespace Launchpad.Warehouse.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigureInfrastructure(this IHostApplicationBuilder app)
    {
        app.ConfigurePersistence();
        app.ConfigureMessageBrokers();

        return app;
    }
}