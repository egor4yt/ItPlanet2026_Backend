namespace Launchpad.Api.Contracts.Employees;

/// <summary>
///     New employee skills
/// </summary>
public class UpdateEmployeeSkillsBody
{
    /// <summary>
    ///     New skills
    /// </summary>
    public required List<UpdateEmployeeSkillsBodyItem> Skills { get; set; }
}

/// <summary>
///     New employee details
/// </summary>
public class UpdateEmployeeSkillsBodyItem
{
    /// <summary>
    ///     Skill id
    /// </summary>
    public required int? SkillId { get; init; }

    /// <summary>
    ///     Skill title
    /// </summary>
    public required string Title { get; init; }
}