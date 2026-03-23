using System;
using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

public class EmployeeFixture : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Employee>(composer => composer
            .With(x => x.Email, () => $"{Guid.NewGuid()}@test.com")
            .With(x => x.RegisteredOn, () => DateTime.UtcNow)
            .Without(x => x.Id)
            .Without(x => x.EmployeeEducations)
            .Without(x => x.EmployeeProjects)
            .Without(x => x.Skills)
        );
    }
}