using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Launchpad.Shared;

public static class GeographyHelper
{
    private static readonly GeometryFactory GeometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

    public static Point CreatePoint(double longitude, double latitude)
    {
        return GeometryFactory.CreatePoint(new Coordinate(longitude, latitude));
    }

    public static Geometry CreateGeometry(Envelope envelope)
    {
        return GeometryFactory.ToGeometry(envelope);
    }
}