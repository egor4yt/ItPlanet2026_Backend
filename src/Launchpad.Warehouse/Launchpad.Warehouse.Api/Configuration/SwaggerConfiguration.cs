using System.Reflection;
using Launchpad.Warehouse.Api.Configuration.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Warehouse.Api.Configuration;

/// <summary>
///     Configure swagger options
/// </summary>
public class SwaggerConfiguration(IConfiguration configuration) : IConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        var swaggerDocOptions = new SwaggerDocOptions();
        configuration.GetSection(nameof(SwaggerDocOptions)).Bind(swaggerDocOptions);


        options.CustomSchemaIds(x => x.FullName);

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = swaggerDocOptions.Title,
            Version = "v1",
            Description = swaggerDocOptions.Description,
            TermsOfService = new Uri("https://github.com"),
            Contact = new OpenApiContact
            {
                Name = swaggerDocOptions.Organization,
                Email = swaggerDocOptions.Email
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://github.com/")
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}