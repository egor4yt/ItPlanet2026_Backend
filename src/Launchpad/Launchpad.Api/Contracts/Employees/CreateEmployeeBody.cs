namespace Launchpad.Api.Contracts.Employees;

/// <summary>
///     New employee details
/// </summary>
public class CreateEmployeeBody
{
    /// <summary>
    ///     employee email
    /// </summary>
    public required string Email { get; init; } = string.Empty;

    /// <summary>
    ///     employee first name
    /// </summary>
    public required string FirstName { get; init; } = string.Empty;

    /// <summary>
    ///     employee last name
    /// </summary>
    public required string LastName { get; init; } = string.Empty;

    /// <summary>
    ///     employee middle name
    /// </summary>
    public string? MiddleName { get; init; } = null;

    /// <summary>
    ///     employee password
    /// </summary>
    public required string Password { get; init; } = string.Empty;
}