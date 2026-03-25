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
}