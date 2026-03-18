namespace Launchpad.Api.Contracts.Employers;

/// <summary>
///     New employer
/// </summary>
public class CreateEmployersBody
{
    /// <summary>
    ///     Employee email
    /// </summary>
    public required string Email { get; init; } = string.Empty;

    /// <summary>
    ///     Employer's company name
    /// </summary>
    public required string CompanyName { get; init; } = string.Empty;

    /// <summary>
    ///     Employer password
    /// </summary>
    public required string Password { get; init; } = string.Empty;
}