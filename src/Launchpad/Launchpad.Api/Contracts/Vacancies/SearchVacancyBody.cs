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

    /// <inheritdoc cref="GeolocationRadiusQuery" />
    public GeolocationRadiusQuery? RadiusSearch { get; set; }

    /// <inheritdoc cref="GeolocationBoxQuery" />
    public GeolocationBoxQuery? BoxSearch { get; set; }
}