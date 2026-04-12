namespace Launchpad.Warehouse.Api.Configuration.Options;

/// <summary>
///     Keycloak options
/// </summary>
public class KeycloakOptions
{
    /// <summary>
    ///     Keycloak authorization url
    /// </summary>
    public string AuthorizationBaseUrl { get; set; } = string.Empty;

    /// <summary>
    ///     Keycloak issuer base urls
    /// </summary>
    public string[] ValidIssuers { get; set; } = [];

    /// <summary>
    ///     Keycloak audience base url
    /// </summary>
    public string AuthorityBaseUrl { get; set; } = string.Empty;

    /// <summary>
    ///     Realm id
    /// </summary>
    public string Realm { get; set; } = string.Empty;

    /// <summary>
    ///     Client id
    /// </summary>
    public string Client { get; set; } = string.Empty;
}