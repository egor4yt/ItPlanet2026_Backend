using Launchpad.Candidates.Infrastructure.Persistence.Configuration;
using Microsoft.Extensions.Hosting;

namespace Launchpad.Candidates.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ConfigureInfrastructure(this IHostApplicationBuilder app)
    {
        app.ConfigurePersistence();

        return app;
    }
}