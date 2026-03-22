using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

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