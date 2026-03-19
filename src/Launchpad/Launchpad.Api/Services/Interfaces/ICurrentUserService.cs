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
    ///     Profile role
    /// </summary>
    public string ProfileRole { get; }

    /// <summary>
    ///     Is user authenticated or not
    /// </summary>
    /// <returns><see langword="true" /> if the user was authenticated, otherwise <see langword="false" /></returns>
    public bool IsAuthenticated { get; }
}