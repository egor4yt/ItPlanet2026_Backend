namespace Launchpad.Api.Services.Interfaces;

/// <summary>
///     Service for interact with current user data
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    ///     User Id
    /// </summary>
    public int UserId { get; }

    /// <summary>
    ///     User email
    /// </summary>
    public string UserEmail { get; }

    /// <summary>
    ///     Is user authenticated or not
    /// </summary>
    /// <returns><see langword="true" /> if the user was authenticated, otherwise <see langword="false" /></returns>
    public bool IsAuthenticated { get; }
}