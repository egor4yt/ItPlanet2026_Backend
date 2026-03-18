using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Fixtures;

public class EmployeeProjectFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<EmployeeProject>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeId)
            .Without(x => x.Employee));
    }
}