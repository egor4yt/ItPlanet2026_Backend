using AutoFixture;
using Launchpad.Shared;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Launchpad.Tests.Base.Fixtures;

public class PointFixture : ICustomization
{
    private readonly GeometryFactory _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
    private readonly Random _random = new Random();

    public void Customize(IFixture fixture)
    {
        fixture.Register(() =>
        {
            var longitude = _random.NextDouble() * 360 - 180;
            var latitude = _random.NextDouble() * 180 - 90;

            var p = GeometryHelper.CreatePoint(longitude, latitude);

            return GeometryHelper.CreatePoint(longitude, latitude);
        });
    }
}