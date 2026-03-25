namespace Launchpad.Api.Contracts.Vacancies;

/// <summary>
///     Create vacancy
/// </summary>
public class CreateVacnacyBody
{
    /// <summary>
    ///     Title
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    ///     Description
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    ///     Longitude
    /// </summary>
    public double Longitude { get; set; }

    /// <summary>
    ///     Latitude
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    ///     Type
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    ///     Start date
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    ///     End date
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    ///     Work formats
    /// </summary>
    public List<int> WorkFormatIds { get; set; } = null!;

    /// <summary>
    ///     Skills
    /// </summary>
    public required List<CreateVacnacyBodySkill> Skills { get; set; }
}

/// <summary>
///     Skill
/// </summary>
public class CreateVacnacyBodySkill
{
    /// <summary>
    ///     Skill id
    /// </summary>
    public required int? Id { get; init; }

    /// <summary>
    ///     Skill title
    /// </summary>
    public required string Title { get; init; }
}