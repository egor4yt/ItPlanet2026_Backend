namespace Launchpad.Domain.Entities;

public class Employee
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string PasswordHash { get; set; } = null!;
    public DateTime RegisteredOn { get; set; }

    public virtual ICollection<EmployeeEducation> EmployeeEducations { get; set; } = null!;
    public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = null!;
    public virtual ICollection<Skill> Skills { get; set; } = null!;
}