namespace Launchpad.Api.Contracts.Employee;

/// <summary>
///     Employee details
/// </summary>
public class AuthorizeEmployeeBody
{
    /// <summary>
    ///     employee email
    /// </summary>
    public required string Email { get; init; } = string.Empty;

    /// <summary>
    ///     employee password
    /// </summary>
    public required string Password { get; init; } = string.Empty;
}