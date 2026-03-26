using Launchpad.Shared;
using NetTopologySuite.Geometries;

namespace Launchpad.Application.SharedModels;

public class GeolocationPoint
{
    public double Longitude { get; init; }
    public double Latitude { get; init; }

    public Point ToNetTopologyPoint()
    {
        return GeographyHelper.CreatePoint(Longitude, Latitude);
    }
}