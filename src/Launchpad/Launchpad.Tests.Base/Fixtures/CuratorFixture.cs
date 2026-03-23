using System;
using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

public class CuratorFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Curator>(composer => composer
            .With(x => x.Email, () => $"{Guid.NewGuid()}@test.com")
            .Without(x => x.Id)
        );
    }
}