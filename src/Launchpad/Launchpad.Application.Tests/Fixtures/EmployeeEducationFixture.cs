using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Fixtures;

public class EmployeeEducationFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<EmployeeEducation>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeId)
            .Without(x => x.EducationLevelId)
            .Without(x => x.Employee)
            .Without(x => x.EducationLevel));
    }
}