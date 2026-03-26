namespace Launchpad.Api.Contracts.Shared;

/// <summary>
///     Represents a query for a geographical bounding box defined by two geolocation points.
/// </summary>
public class GeolocationBoxQuery
{
    /// <summary>
    ///     Gets the starting geolocation point of the bounding box.
    ///     This represents one corner of the geographical area being queried.
    /// </summary>
    public required GeolocationPoint From { get; init; }

    /// <summary>
    ///     Gets the endings geolocation point of the bounding box.
    ///     This represents either corner of the geographical area being queried.
    /// </summary>
    public required GeolocationPoint To { get; init; }

    /// <summary>
    ///     Converts the current instance to its corresponding application model representation.
    /// </summary>
    public Application.SharedModels.GeolocationBoxQuery ToApplicationModel()
    {
        return new Application.SharedModels.GeolocationBoxQuery
        {
            From = From.ToApplicationModel(),
            To = To.ToApplicationModel()
        };
    }
}