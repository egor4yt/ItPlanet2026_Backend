using System;
using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

public class EmployerFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Employer>(composer => composer
            .With(x => x.Email, () => $"{Guid.NewGuid()}@test.com")
            .With(x => x.RegisteredOn, () => DateTime.UtcNow)
            .Without(x => x.Id)
            .Without(x => x.ActivityFields)
            .Without(x => x.Verification)
        );
    }
}