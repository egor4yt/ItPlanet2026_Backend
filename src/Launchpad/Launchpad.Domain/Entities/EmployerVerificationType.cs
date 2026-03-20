namespace Launchpad.Domain.Entities;

public class EmployerVerificationType
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string HtmlDescription { get; set; }

    public virtual ICollection<EmployerVerification> EmployerVerifications { get; set; }
}