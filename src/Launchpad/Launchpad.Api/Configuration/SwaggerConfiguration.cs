using System.Reflection;
using Launchpad.Api.Configuration.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Api.Configuration;

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

        const string authorizationScheme = "Bearer";

        options.AddSecurityDefinition(authorizationScheme, new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Description = "Please provide a valid token",
            Name = "JWT Bearer authorization",
            In = ParameterLocation.Header,
            Scheme = authorizationScheme,
            BearerFormat = "JWT"
        });

        options.AddSecurityRequirement(document =>
        {
            var requirement = new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(authorizationScheme, document)] = []
            };
            
            return requirement;
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}