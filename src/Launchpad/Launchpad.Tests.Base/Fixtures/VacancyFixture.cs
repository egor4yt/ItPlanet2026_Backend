using AutoFixture;
using Launchpad.Domain.Entities;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace Launchpad.Tests.Base.Fixtures;

public class VacancyFixture : ICustomization
{
    private readonly GeometryFactory _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);
    private readonly Random _random = new Random();
    private readonly int[] _vacancyTypeIds =
    [
        Domain.Metadata.VacancyTypeId.Event,
        Domain.Metadata.VacancyTypeId.Vacancy,
        Domain.Metadata.VacancyTypeId.Intership,
        Domain.Metadata.VacancyTypeId.Mentoring
    ];

    public void Customize(IFixture fixture)
    {
        fixture.Customize<Vacancy>(composer => composer
            .With(x => x.Location, () =>
            {
                var longitude = _random.NextDouble() * 360 - 180;
                var latitude = _random.NextDouble() * 180 - 90;

                return _geometryFactory.CreatePoint(new Coordinate(longitude, latitude));
            })
            .With(x => x.CreatedAt, () => DateTime.UtcNow)
            .With(x => x.TypeId, _vacancyTypeIds[_random.Next(0, _vacancyTypeIds.Length)])
            .Without(x => x.Id)
            .Without(x => x.Employer)
            .Without(x => x.EmployerId)
            .Without(x => x.TypeId)
            .Without(x => x.Type)
        );
    }
}