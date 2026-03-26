namespace Launchpad.Api.Contracts.EmployeeResponds;

/// <summary>
///     New employee respond
/// </summary>
public class CreateEmployeeRespondBody
{
    /// <summary>
    ///     Employee id
    /// </summary>
    public long EmployeeId { get; set; }

    /// <summary>
    ///     Vacancy id
    /// </summary>
    public long VacancyId { get; set; }

    /// <summary>
    ///     Cover message
    /// </summary>
    public string? CoverMessage { get; set; }
}