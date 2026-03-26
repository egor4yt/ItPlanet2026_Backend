using Launchpad.Shared;
using NetTopologySuite.Geometries;

namespace Launchpad.Application.SharedModels;

public class GeolocationBoxQuery
{
    public required GeolocationPoint From { get; init; }
    public required GeolocationPoint To { get; init; }

    public Geometry ToNetTopologyGeometry()
    {
        var envelope = new Envelope(From.Longitude, To.Longitude, From.Latitude, To.Latitude);
        return GeographyHelper.CreateGeometry(envelope);
    }
}