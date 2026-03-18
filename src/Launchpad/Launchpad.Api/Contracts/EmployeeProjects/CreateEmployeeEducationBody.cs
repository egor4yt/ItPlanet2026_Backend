namespace Launchpad.Api.Contracts.EmployeeProjects;

/// <summary>
///     New employee project
/// </summary>
public class CreateEmployeeProjectBody
{
    /// <summary>
    ///     Tilte
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    ///     Description
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    ///     Specialization
    /// </summary>
    public string Specialization { get; set; } = null!;

    /// <summary>
    ///     Link
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    ///     DateFrom
    /// </summary>
    public DateOnly DateFrom { get; set; }

    /// <summary>
    ///     DateTo
    /// </summary>
    public DateOnly DateTo { get; set; }
}