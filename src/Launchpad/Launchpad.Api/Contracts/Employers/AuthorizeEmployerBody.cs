namespace Launchpad.Api.Contracts.Employers;

/// <summary>
///     Employer authorization details
/// </summary>
public class AuthorizeEmployerBody
{
    /// <summary>
    ///     Employee email
    /// </summary>
    public required string Email { get; init; } = string.Empty;

    /// <summary>
    ///     Employer password
    /// </summary>
    public required string Password { get; init; } = string.Empty;
}