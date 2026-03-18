namespace Launchpad.Api.Contracts.EmployeeEducations;

/// <summary>
///     New employee education
/// </summary>
public class UpdateEmployeeEducationBody
{
    /// <summary>
    ///     Organization
    /// </summary>
    public string Organization { get; set; } = null!;

    /// <summary>
    ///     Faculty
    /// </summary>
    public string Faculty { get; set; } = null!;

    /// <summary>
    ///     Specialization
    /// </summary>
    public string Specialization { get; set; } = null!;

    /// <summary>
    ///     Completion year or prediction
    /// </summary>
    public int CompletionYear { get; set; }

    /// <summary>
    ///     Education level id
    /// </summary>
    public int EducationLevelId { get; set; }
}