using AutoFixture;
using Launchpad.Domain.Entities;

namespace Launchpad.Tests.Base.Fixtures;

public class EmployerVerificationFixture : ICustomization
{
    private readonly Random _random = new Random();
    private readonly int[] _verificationStatusIds =
    [
        Domain.Metadata.EmployerVerificationStatusId.Approved,
        Domain.Metadata.EmployerVerificationStatusId.Pending,
        Domain.Metadata.EmployerVerificationStatusId.Rejected,
        Domain.Metadata.EmployerVerificationStatusId.WaitingEmployer
    ];
    private readonly int[] _verificationTypeIds =
    [
        Domain.Metadata.EmployerVerificationTypeId.Organization,
        Domain.Metadata.EmployerVerificationTypeId.PrivatePerson,
        Domain.Metadata.EmployerVerificationTypeId.PrivateRecruiter,
        Domain.Metadata.EmployerVerificationTypeId.Project,
        Domain.Metadata.EmployerVerificationTypeId.RecruitmentAgency,
        Domain.Metadata.EmployerVerificationTypeId.SelfEmployed
    ];

    public void Customize(IFixture fixture)
    {
        fixture.Customize<EmployerVerification>(composer => composer
            .With(x => x.StatusId, _verificationStatusIds[_random.Next(0, _verificationStatusIds.Length)])
            .With(x => x.EmployerVerificationTypeId, _verificationTypeIds[_random.Next(0, _verificationTypeIds.Length)])
            .With(x => x.ChangedOn, DateTime.UtcNow)
            .With(x => x.TaxpayerIndividualNumber, Guid.NewGuid().ToString("N")[..12])
            .Without(x => x.Id)
            .Without(x => x.Status)
            .Without(x => x.Type)
            .Without(x => x.EmployerId)
            .Without(x => x.Employer)
        );
    }
}