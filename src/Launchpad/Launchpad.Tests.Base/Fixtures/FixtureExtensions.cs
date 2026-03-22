using AutoFixture;

namespace Launchpad.Tests.Base.Fixtures;

public static class FixtureExtensions
{
    public static void RegisterAllFixtureCustomizations(this IFixture fixture)
    {
        fixture.Customize(new EmployeeFixture());
        fixture.Customize(new EducationLevelFixture());
        fixture.Customize(new EmployeeEducationFixture());
        fixture.Customize(new SkillFixture());
        fixture.Customize(new EmployeeProjectFixture());
        fixture.Customize(new EmployerFixture());
        fixture.Customize(new DateOnlyFixture());
        fixture.Customize(new ActivityFieldGroupFixture());
        fixture.Customize(new ActivityFieldFixture());
    }
}