using Launchpad.Api.Contracts.Shared;

namespace Launchpad.Api.Contracts.Vacancies;

/// <summary>
///     Search vacancies request
/// </summary>
public class SearchVacancyBody
{
    /// <summary>
    ///     Part of the vacancy title (ignore-case)
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     A collection of unique identifiers for vacancies that should be explicitly included in the search results.
    /// </summary>
    public required List<long>? IncludeIds { get; init; }

    /// <inheritdoc cref="GeolocationRadiusQuery" />
    public GeolocationRadiusQuery? RadiusSearch { get; set; }

    /// <inheritdoc cref="GeolocationBoxQuery" />
    public GeolocationBoxQuery? BoxSearch { get; set; }
}