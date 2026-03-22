namespace Launchpad.Domain.Entities;

public sealed class EducationLevel
{
    public int Id { get; init; }
    public required string Title { get; init; }

    public ICollection<EmployeeEducation> EmployeeEducations { get; init; } = [];
}