using System.Diagnostics;
using Launchpad.Shared;
using NetTopologySuite.Geometries;

namespace Launchpad.Application.SharedModels;

[DebuggerDisplay("Point ({Longitude} {Latitude})")]
public class GeolocationPoint
{
    public double Longitude { get; init; }
    public double Latitude { get; init; }

    public Point ToNetTopologyPoint()
    {
        return GeographyHelper.CreatePoint(Longitude, Latitude);
    }
}