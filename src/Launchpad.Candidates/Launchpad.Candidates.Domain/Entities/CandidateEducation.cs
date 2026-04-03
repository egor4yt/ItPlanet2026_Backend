namespace Launchpad.Candidates.Domain.Entities;

public sealed class CandidateEducation
{
    public long Id { get; init; }
    public required string Organization { get; init; }
    public required string Faculty { get; init; }
    public required string Specialization { get; init; }
    public int CompletionYear { get; init; }

    public int EducationLevelId { get; set; }
    public long EmployeeId { get; set; }

    public EducationLevel? EducationLevel { get; init; }
    public Candidate? Employee { get; init; }
}