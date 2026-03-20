namespace Launchpad.Domain.Entities;

public class EmployerVerification
{
    public long Id { get; set; }
    public string RequestMessage { get; set; } = null!;
    public string? ResponseMessage { get; set; } = null!;
    public string? TaxpayerIndividualNumber { get; set; }
    public string? SocialNetworkLink { get; set; }
    public DateTime ChangedOn { get; set; }

    public long EmployerId { get; set; }
    public int StatusId { get; set; }
    public int EmployerVerificationTypeId { get; set; }

    public virtual Employer Employer { get; set; }
    public virtual EmployerVerificationStatus Status { get; set; }
    public virtual EmployerVerificationType Type { get; set; }
}