using AutoFixture;

namespace Launchpad.Tests.Base.Fixtures;

public class DateOnlyFixture : ICustomization
{
    private readonly Random _random = new Random();

    public void Customize(IFixture fixture)
    {
        fixture.Customize<DateOnly>(composer => composer
            .FromFactory<DateTime>(DateOnly.FromDateTime)
        );
    }
}