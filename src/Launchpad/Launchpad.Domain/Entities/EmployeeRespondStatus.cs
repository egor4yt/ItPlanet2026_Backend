namespace Launchpad.Domain.Entities;

public sealed class EmployeeRespondStatus
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }

    public int ColorId { get; set; }
    
    public Color? Color { get; init; }
    public ICollection<EmployeeRespond> EmployeeResponds { get; init; } = [];
}