namespace Launchpad.Candidates.Api.Services.Interfaces;

/// <summary>
///     Service for interact with current user data
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     Keycloak user id (parameter 'sub' in Bearer token)
    /// </summary>
    public Guid IdentityId { get; }

    /// <summary>
    ///     Keycloak user email (parameter 'email' in Bearer token)
    /// </summary>
    public string IdentityEmail { get; }

    /// <summary>
    ///     Is a user authenticated or not?
    /// </summary>
    /// <returns><see langword="true" /> if the user was authenticated, otherwise <see langword="false" /></returns>
    public bool IsAuthenticated { get; }

    /// <summary>
    ///     Checks if the current user belongs to the specified role.
    /// </summary>
    /// <param name="role">The role to check against the current user's roles.</param>
    /// <returns>True if the current user belongs to the specified role, otherwise false.</returns>
    bool IsInRole(string role);
}