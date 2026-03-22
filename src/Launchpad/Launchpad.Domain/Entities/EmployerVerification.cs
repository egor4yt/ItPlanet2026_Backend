namespace Launchpad.Domain.Entities;

public sealed class EmployerVerification
{
    public long Id { get; init; }
    public required string RequestMessage { get; init; }
    public string? ResponseMessage { get; init; }
    public string? TaxpayerIndividualNumber { get; init; }
    public string? SocialNetworkLink { get; init; }
    public DateTime ChangedOn { get; init; }

    public long EmployerId { get; init; }
    public int StatusId { get; init; }
    public int EmployerVerificationTypeId { get; init; }

    public Employer? Employer { get; init; }
    public EmployerVerificationStatus? Status { get; init; }
    public EmployerVerificationType? Type { get; init; }
}