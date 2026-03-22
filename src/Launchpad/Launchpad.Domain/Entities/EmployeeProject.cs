namespace Launchpad.Domain.Entities;

public sealed class EmployeeProject
{
    public long Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required string Specialization { get; init; }
    public string? Link { get; init; }
    public DateOnly DateFrom { get; init; }
    public DateOnly DateTo { get; init; }
    public long EmployeeId { get; set; }
    public Employee? Employee { get; init; }
}