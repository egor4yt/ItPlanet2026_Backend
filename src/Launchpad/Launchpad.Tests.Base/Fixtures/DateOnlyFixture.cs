using AutoFixture;

namespace Launchpad.Tests.Base.Fixtures;

public class DateOnlyFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<DateOnly>(composer => composer.FromFactory<DateTime>(DateOnly.FromDateTime));
    }
}