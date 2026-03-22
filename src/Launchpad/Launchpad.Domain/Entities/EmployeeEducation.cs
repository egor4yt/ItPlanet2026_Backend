namespace Launchpad.Domain.Entities;

public sealed class EmployeeEducation
{
    public long Id { get; init; }
    public required string Organization { get; init; }
    public required string Faculty { get; init; }
    public required string Specialization { get; init; }
    public int CompletionYear { get; init; }

    public int EducationLevelId { get; set; }
    public long EmployeeId { get; set; }

    public EducationLevel? EducationLevel { get; init; }
    public Employee? Employee { get; init; }
}