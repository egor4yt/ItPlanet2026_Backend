namespace Launchpad.Domain.Entities;

public sealed class Color
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Code { get; init; }

    public ICollection<EmployeeRespondStatus> EmployeeRespondStatuses { get; init; } = [];
}