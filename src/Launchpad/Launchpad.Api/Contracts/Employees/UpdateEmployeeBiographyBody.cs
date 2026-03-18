namespace Launchpad.Api.Contracts.Employees;

/// <summary>
///     New employee skills
/// </summary>
public class UpdateEmployeeBiographyBody
{
    /// <summary>
    ///     New biography
    /// </summary>
    public required string Biography { get; set; }
}