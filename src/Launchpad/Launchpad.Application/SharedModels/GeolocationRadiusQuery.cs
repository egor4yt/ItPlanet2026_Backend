namespace Launchpad.Application.SharedModels;

public class GeolocationRadiusQuery
{
    public required GeolocationPoint Point { get; init; }
    public double RadiusInMeters { get; init; }
}