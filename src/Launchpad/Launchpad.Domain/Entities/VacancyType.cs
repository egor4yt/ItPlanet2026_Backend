namespace Launchpad.Domain.Entities;

public sealed class VacancyType
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public ICollection<Vacancy> Vacancies { get; init; } = [];
}