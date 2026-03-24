using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

public class EducationLevelFixture : ICustomization
{
    private static int _id = 100;

    public void Customize(IFixture fixture)
    {
        fixture.Customize<EducationLevel>(composer => composer
            .With(x => x.Id, () => Interlocked.Increment(ref _id))
            .Without(x => x.EmployeeEducations));
    }
}