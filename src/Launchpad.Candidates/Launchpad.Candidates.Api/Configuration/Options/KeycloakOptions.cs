namespace Launchpad.Candidates.Api.Configuration.Options;

/// <summary>
///     Keycloak options
/// </summary>
public class KeycloakOptions
{
    /// <summary>
    ///     Keycloak issuer base url
    /// </summary>
    public string IssuerBaseUrl { get; set; } = string.Empty;

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