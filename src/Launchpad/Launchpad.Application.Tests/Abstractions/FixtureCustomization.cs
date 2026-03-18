using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Application.Tests.Abstractions;

public class FixtureCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));

        fixture.Customize<Employee>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeEducations)
            .Without(x => x.EmployeeProjects)
            .Without(x => x.Skills));
        
        fixture.Customize<EducationLevel>(composer => composer
            .Without(x => x.EmployeeEducations)
            .With(x => x.Title, () => "Level " + Guid.NewGuid()));
        
        fixture.Customize<EmployeeEducation>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeId)
            .Without(x => x.EducationLevelId)
            .Without(x => x.Employee)
            .Without(x => x.EducationLevel));
        
        fixture.Customize<Skill>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.Employees));
        
        fixture.Customize<EmployeeProject>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.EmployeeId)
            .Without(x => x.Employee));
        
        fixture.Customize<Employer>(composer => composer
            .Without(x => x.Id));
    }
}
