using System.Reflection;
using Launchpad.Candidates.Api.Configuration.Options;
using Launchpad.Candidates.Shared;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Launchpad.Candidates.Api.Configuration;

/// <summary>
///     Configure swagger options
/// </summary>
public class SwaggerConfiguration(IConfiguration configuration) : IConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        var keycloakOptions = configuration
            .GetRequiredSection(ConfigurationKeys.Keyckloak)
            .Get<KeycloakOptions>()!;

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

        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{keycloakOptions.BaseUrl}/realms/{keycloakOptions.Realm}/protocol/openid-connect/auth"),
                    TokenUrl = new Uri($"{keycloakOptions.BaseUrl}/realms/{keycloakOptions.Realm}/protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        ["api-audience"] = "Add audience"
                    }
                }
            }
        });

        options.AddSecurityRequirement(document =>
        {
            var requirement = new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("oauth2", document)] = []
            };

            return requirement;
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}