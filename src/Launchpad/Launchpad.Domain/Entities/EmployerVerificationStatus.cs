namespace Launchpad.Domain.Entities;

public class EmployerVerificationStatus
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public virtual ICollection<EmployerVerification> EmployerVerifications { get; set; }
}