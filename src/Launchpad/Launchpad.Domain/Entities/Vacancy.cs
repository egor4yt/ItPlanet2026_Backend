namespace Launchpad.Domain.Entities;

public class Vacancy
{
    public long Id { get; set; }
    public long EmployerId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public virtual ICollection<Employee> Employers { get; set; } = null!;
}