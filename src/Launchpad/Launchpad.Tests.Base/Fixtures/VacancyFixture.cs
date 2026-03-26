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
            .With(x => x.CreatedAt, () => DateTime.UtcNow)
            .With(x => x.StartDate, () => DateTime.UtcNow)
            .With(x => x.EndDate, () => DateTime.UtcNow)
            .With(x => x.TypeId, _vacancyTypeIds[_random.Next(0, _vacancyTypeIds.Length)])
            .Without(x => x.Id)
            .Without(x => x.Employer)
            .Without(x => x.EmployerId)
            .Without(x => x.Type)
            .Without(x => x.WorkFormats)
            .Without(x => x.Skills)
            .Without(x => x.EmployeeResponds)
        );
    }
}