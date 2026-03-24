namespace Launchpad.Domain.Entities;

public sealed class Employer
{
    public long Id { get; init; }
    public required string Email { get; set; }
    public required string CompanyName { get; set; }
    public required string PasswordHash { get; init; }
    public DateTime RegisteredOn { get; init; }

    public string? Description { get; set; }
    public string? Website { get; init; }

    public ICollection<ActivityField> ActivityFields { get; set; } = [];
    public ICollection<Vacancy> Vacancies { get; set; } = [];
    public EmployerVerification? Verification { get; set; }
}