namespace Launchpad.Domain.Entities;

public class EmployeeProject
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Specialization { get; set; } = null!;
    public string? Link { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public long EmployeeId { get; set; }
    public virtual Employee? Employee { get; set; }
}