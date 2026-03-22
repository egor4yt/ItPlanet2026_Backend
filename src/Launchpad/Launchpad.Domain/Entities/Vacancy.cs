namespace Launchpad.Domain.Entities;

public sealed class Vacancy
{
    public long Id { get; set; }
    public long EmployerId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }

    public ICollection<Employee> Employers { get; set; } = [];
}