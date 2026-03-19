using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Fixtures;

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