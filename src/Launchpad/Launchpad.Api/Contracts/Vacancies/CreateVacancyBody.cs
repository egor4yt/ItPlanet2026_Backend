using Launchpad.Api.Contracts.Shared;

namespace Launchpad.Api.Contracts.Vacancies;

/// <summary>
///     Create vacancy
/// </summary>
public class CreateVacancyBody
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
    ///     City
    /// </summary>
    public string City { get; set; } = null!;

    /// <summary>
    ///     Full address
    /// </summary>
    public string FullAddress { get; set; } = null!;

    /// <summary>
    ///     Vacancy work location
    /// </summary>
    public GeolocationPoint Location { get; set; } = null!;

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