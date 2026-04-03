namespace Launchpad.Candidates.Domain.Entities;

public sealed class EducationLevel
{
    public int Id { get; init; }
    public required string Title { get; init; }

    public ICollection<CandidateEducation> EmployeeEducations { get; init; } = [];
}