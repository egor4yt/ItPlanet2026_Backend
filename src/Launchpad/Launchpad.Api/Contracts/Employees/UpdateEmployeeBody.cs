namespace Launchpad.Api.Contracts.Employees;

/// <summary>
///     New employee details
/// </summary>
public class UpdateEmployeeBody
{
    /// <summary>
    ///     Employee's first name
    /// </summary>
    public required string FirstName { get; init; } = string.Empty;

    /// <summary>
    ///     Employee's last name
    /// </summary>
    public required string LastName { get; init; } = string.Empty;

    /// <summary>
    ///     Employee's middle name
    /// </summary>
    public string? MiddleName { get; init; } = null;

    /// <summary>
    ///     Employee's birthdate
    /// </summary>
    public DateOnly? BirthDate { get; init; }

    /// <summary>
    ///     Employee's gender
    /// </summary>
    public bool IsMale { get; init; }
}