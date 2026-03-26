namespace Launchpad.Api.Contracts.Shared;

/// <summary>
///     Represents a query for a geographical circe defined with one geolocation point and radius
/// </summary>
public class GeolocationRadiusQuery
{
    /// <summary>
    ///     The center point of the circle
    /// </summary>
    public required GeolocationPoint Point { get; init; }

    /// <summary>
    ///     The radios of the circle
    /// </summary>
    public double Radius { get; init; }

    /// <summary>
    ///     Converts the current instance to its corresponding application model representation.
    /// </summary>
    public Application.SharedModels.GeolocationRadiusQuery ToApplicationModel()
    {
        return new Application.SharedModels.GeolocationRadiusQuery
        {
            Point = Point.ToApplicationModel(),
            RadiusInMeters = Radius
        };
    }
}