using NetTopologySuite.Geometries;

namespace Launchpad.Domain.Entities;

public sealed class Vacancy
{
    public long Id { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required Point Location { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }

    public int TypeId { get; init; }
    public long EmployerId { get; init; }
    public Employer? Employer { get; init; }
    public VacancyType? Type { get; init; }
    public ICollection<WorkFormat> WorkFormats { get; init; } = [];
    public ICollection<Skill> Skills { get; init; } = [];
}