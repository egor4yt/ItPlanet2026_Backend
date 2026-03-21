using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Fixtures;

public class ActivityFieldFixture : ICustomization
{
    private static int _id = 100;

    public void Customize(IFixture fixture)
    {
        fixture.Customize<ActivityField>(composer => composer
            .With(x => x.Id, () => Interlocked.Increment(ref _id))
            .Without(x => x.ActivityFieldGroup)
            .Without(x => x.ActivityFieldGroupId)
        );
    }
}