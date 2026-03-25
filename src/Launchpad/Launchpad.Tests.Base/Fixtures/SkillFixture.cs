using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

public class SkillFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Skill>(composer => composer
            .Without(x => x.Id)
            .Without(x => x.Employees)
            .Without(x => x.Vacancies)
        );
    }
}