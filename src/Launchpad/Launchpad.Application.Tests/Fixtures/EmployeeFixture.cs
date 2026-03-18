using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Fixtures;

public class EmployeeFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Employee>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeEducations)
            .Without(x => x.EmployeeProjects)
            .Without(x => x.Skills));
    }
}