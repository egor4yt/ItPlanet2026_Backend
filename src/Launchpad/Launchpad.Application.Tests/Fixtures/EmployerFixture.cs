using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Fixtures;

public class EmployerFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Employer>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.ActivityFields)
            .Without(x => x.Verification)
        );
    }
}