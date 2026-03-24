using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

public class EmployerFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Employer>(composer => composer
            .With(x => x.Email, () => $"{Guid.NewGuid()}@test.com")
            .With(x => x.RegisteredOn, () =>
            {
                var now = DateTime.UtcNow;
                return new DateTime(now.Ticks - now.Ticks % 10, now.Kind);
            })
            .Without(x => x.Id)
            .Without(x => x.ActivityFields)
            .Without(x => x.Verification)
            .Without(x => x.Vacancies)
        );
    }
}