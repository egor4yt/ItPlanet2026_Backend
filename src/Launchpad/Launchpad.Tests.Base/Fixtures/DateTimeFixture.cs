using AutoFixture;

namespace Launchpad.Tests.Base.Fixtures;

public class DateTimeFixture : ICustomization
{
    private readonly Random _random = new Random();

    public void Customize(IFixture fixture)
    {
        fixture.Register(() =>
        {
            var dateTime = DateTime.UtcNow.AddDays(-_random.Next(365 * 16, 365 * 40));
            return new DateTime(dateTime.Ticks - dateTime.Ticks % 10, dateTime.Kind);
        });
    }
}