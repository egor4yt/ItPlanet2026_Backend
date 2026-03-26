using AutoFixture;
using Launchpad.Api.Contracts.Shared;
using NetTopologySuite.Geometries;

namespace Launchpad.Tests.Base.Fixtures;

public class GeolocationPointFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<GeolocationPoint>(composer => composer
            .FromFactory((Point point) => new GeolocationPoint
            {
                Longitude = point.Coordinate.X,
                Latitude = point.Coordinate.Y
            })
            .OmitAutoProperties()
        );
    }
}