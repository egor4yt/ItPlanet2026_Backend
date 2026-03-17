namespace Launchpad.Domain.Entities;

public class Skill
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public bool IsSystemTag { get; set; }
    
    public virtual ICollection<Employee> Employees { get; set; } = null!;
}