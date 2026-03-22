namespace Launchpad.Domain.Entities;

public sealed class Skill
{
    public int Id { get; init; }
    public required string Title { get; set; }
    public bool IsSystemTag { get; init; }

    public ICollection<Employee> Employees { get; init; } = [];
}