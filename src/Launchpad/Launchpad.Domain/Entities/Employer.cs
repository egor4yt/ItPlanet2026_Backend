namespace Launchpad.Domain.Entities;

public class Employer
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime RegisteredOn { get; set; }

    public string? Description { get; set; }
    public string? Website { get; set; }

    public virtual ICollection<ActivityField> ActivityFields { get; set; }
    public virtual EmployerVerification? Verification { get; set; }
}