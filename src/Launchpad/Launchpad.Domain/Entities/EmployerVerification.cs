namespace Launchpad.Domain.Entities;

public class EmployerVerification
{
    public long Id { get; set; }
    public string RequestMessage { get; set; } = null!;
    public string ResponseMessage { get; set; } = null!;
    public DateTime ChangedOn { get; set; }
    
    public long EmployerId { get; set; }
    public int StatusId { get; set; }

    public virtual Employer Employer { get; set; }
    public virtual EmployerVerificationStatus EmployerVerificationStatus { get; set; }
}