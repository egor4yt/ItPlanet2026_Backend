namespace Launchpad.Api.Contracts.Shared;

/// <summary>
///     Geolocation point representation
/// </summary>
public class GeolocationPoint
{
    /// <summary>
    ///     Longitude of the point
    /// </summary>
    public double Longitude { get; init; }

    /// <summary>
    ///     Latitude of the point
    /// </summary>
    public double Latitude { get; init; }

    /// <summary>
    ///     Converts the current instance to its corresponding application model representation.
    /// </summary>
    public Application.SharedModels.GeolocationPoint ToApplicationModel()
    {
        return new Application.SharedModels.GeolocationPoint
        {
            Longitude = Longitude,
            Latitude = Latitude
        };
    }
}