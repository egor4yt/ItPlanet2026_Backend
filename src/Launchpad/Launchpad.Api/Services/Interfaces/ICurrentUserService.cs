namespace Launchpad.Api.Services.Interfaces;

/// <summary>
///     Service for interact with current user data
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     Profile ID
    /// </summary>
    public long ProfileId { get; }

    /// <summary>
    ///     Contact email
    /// </summary>
    public string ContactEmail { get; }

    /// <summary>
    ///     Is user authenticated or not
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